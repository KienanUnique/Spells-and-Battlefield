using System;
using System.Collections;
using Common.Interfaces;
using Common.Readonly_Transform;
using Enemies.Controller;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors;
using Spells.Factory;
using Spells.Spell;
using UnityEngine;
using Zenject;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells
{
    public class UseSpellsStateEnemyAI : StateEnemyAI, ICoroutineStarter, IReadonlySpellSelector
    {
        [SerializeField] private UseSpellsStateData _useSpellsStateData;
        [SerializeField] private EnemyController _enemyController;
        [SerializeField] private ReadonlyTransformGetter _spellSpawnObjectReadonlyTransformGetter;
        private ISpell _cachedSpell;
        private ILookPointCalculator _currentLookPointCalculator;
        private bool _isWaitingForAnimationFinish;
        private IReadonlyTransform _previousThisPositionReferencePointTransform;
        private ISpellObjectsFactory _spellObjectsFactory;
        private IReadonlyTransform _spellSpawnPoint;
        private ISpellSelector _spellsSelector;
        private Coroutine _tryUseSpellCoroutine;

        [Inject]
        private void GetDependencies(ISpellObjectsFactory spellObjectsFactory)
        {
            _spellObjectsFactory = spellObjectsFactory;
        }

        public event Action CanUseSpellsAgain;
        public override ILookPointCalculator LookPointCalculator => _currentLookPointCalculator;
        public bool CanUseSpell => _spellsSelector.CanUseSpell;

        private Quaternion SpellSpawnDirection =>
            Quaternion.LookRotation(StateMachineControllable.CurrentLookDirection);

        protected override void SpecialInitializeAction()
        {
            base.SpecialInitializeAction();
            _currentLookPointCalculator = new FollowTargetLookPointCalculator();
            _spellsSelector = _useSpellsStateData.GetSpellSelector(this);
            _spellSpawnPoint = _spellSpawnObjectReadonlyTransformGetter.ReadonlyTransform;
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _spellsSelector.CanUseSpellsAgain += OnCanUseSpellsAgain;

            if (CurrentStatus == StateEnemyAIStatus.Active)
            {
                SubscribeOnLocalEvents();
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            _spellsSelector.CanUseSpellsAgain -= OnCanUseSpellsAgain;
            UnsubscribeFromLocalEvents();
        }

        protected override void SpecialReactionOnStateStatusChange(StateEnemyAIStatus newStatus)
        {
            switch (newStatus)
            {
                case StateEnemyAIStatus.NonActive:
                    UnsubscribeFromLocalEvents();
                    if (_tryUseSpellCoroutine != null)
                    {
                        StopCoroutine(_tryUseSpellCoroutine);
                        _tryUseSpellCoroutine = null;
                    }

                    StateMachineControllable.ChangeThisPositionReferencePointTransform(
                        _previousThisPositionReferencePointTransform);
                    StateMachineControllable.StopMoving();
                    _isWaitingForAnimationFinish = false;
                    break;

                case StateEnemyAIStatus.Active:
                    _isWaitingForAnimationFinish = false;
                    _previousThisPositionReferencePointTransform =
                        StateMachineControllable.ThisPositionReferencePointForLook;
                    StateMachineControllable.ChangeThisPositionReferencePointTransform(_spellSpawnPoint);
                    StateMachineControllable.StartKeepingCurrentTargetOnDistance(_useSpellsStateData.DataForMoving);
                    SubscribeOnLocalEvents();
                    _tryUseSpellCoroutine = StartCoroutine(TryCastSelectedSpellContinuously());
                    break;

                case StateEnemyAIStatus.Exiting:
                    if (!_isWaitingForAnimationFinish)
                    {
                        HandleExitFromState();
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }

        protected override void ChangeLookPointCalculator(ILookPointCalculator newCalculator)
        {
            _currentLookPointCalculator = newCalculator;
            base.ChangeLookPointCalculator(_currentLookPointCalculator);
        }

        private IEnumerator TryCastSelectedSpellContinuously()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                TryCastSelectedSpell();
                yield return waitForFixedUpdate;
            }
        }

        private void SubscribeOnLocalEvents()
        {
            _spellsSelector.Enable();
            StateMachineControllable.ActionAnimationEnd += OnActionAnimationEnd;
            StateMachineControllable.ActionAnimationKeyMomentTrigger += OnActionAnimationKeyMomentTrigger;
        }

        private void UnsubscribeFromLocalEvents()
        {
            _spellsSelector.Disable();
            StateMachineControllable.ActionAnimationEnd -= OnActionAnimationEnd;
            StateMachineControllable.ActionAnimationKeyMomentTrigger -= OnActionAnimationKeyMomentTrigger;
        }

        private void OnActionAnimationEnd()
        {
            _isWaitingForAnimationFinish = false;
            switch (CurrentStatus)
            {
                case StateEnemyAIStatus.Exiting:
                    HandleExitFromState();
                    return;
                case StateEnemyAIStatus.Active:
                    HandleCompletedAction();
                    if (!_spellsSelector.CanUseSpell)
                    {
                        _currentLookPointCalculator = new FollowTargetLookPointCalculator();
                        ChangeLookPointCalculator(_currentLookPointCalculator);
                    }

                    TryCastSelectedSpell();
                    break;
                case StateEnemyAIStatus.NonActive:
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void OnActionAnimationKeyMomentTrigger()
        {
            CreateSelectedSpell();
        }

        private void OnCanUseSpellsAgain()
        {
            CanUseSpellsAgain?.Invoke();
            TryCastSelectedSpell();
        }

        private void TryCastSelectedSpell()
        {
            if (CurrentStatus != StateEnemyAIStatus.Active ||
                _isWaitingForAnimationFinish ||
                !_spellsSelector.CanUseSpell)
            {
                return;
            }

            _isWaitingForAnimationFinish = true;
            _cachedSpell = _spellsSelector.Pop();
            ChangeLookPointCalculator(_cachedSpell.LookPointCalculator);
            StateMachineControllable.PlayActionAnimation(_cachedSpell.SpellAnimationData);
        }

        private void CreateSelectedSpell()
        {
            _spellObjectsFactory.Create(_cachedSpell.SpellDataForSpellController, _cachedSpell.SpellPrefabProvider,
                _enemyController, _spellSpawnPoint.Position, SpellSpawnDirection);
        }
    }
}