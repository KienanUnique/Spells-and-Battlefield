using System;
using Common.Readonly_Transform;
using Enemies.Controller;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors;
using Interfaces;
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
        [SerializeField] private ReadonlyTransformGetter _spellAimPointReadonlyTransformGetter;
        private ISpellSelector _spellsSelector;
        private ILookPointCalculator _currentLookPointCalculator;
        private ISpell _cachedSpell;
        private bool _isWaitingForAnimationFinish;
        private ISpellObjectsFactory _spellObjectsFactory;
        private IReadonlyTransform _spellSpawnObject;
        private IReadonlyTransform _spellAimPoint;
        private IReadonlyTransform _previousThisPositionReferencePointTransform;
        private bool _wasSpellCasted;

        [Inject]
        private void Construct(ISpellObjectsFactory spellObjectsFactory)
        {
            _spellObjectsFactory = spellObjectsFactory;
        }

        public event Action CanUseSpellsAgain;
        public bool CanUseSpell => _spellsSelector.CanUseSpell;
        public override ILookPointCalculator LookPointCalculator => _currentLookPointCalculator;
        private Quaternion SpellSpawnDirection => StateMachineControllable.ReadonlyRigidbody.Rotation;

        protected override void SpecialEnterAction()
        {
            _isWaitingForAnimationFinish = false;
            _wasSpellCasted = false;
            _previousThisPositionReferencePointTransform = StateMachineControllable.ThisPositionReferencePointForLook;
            StateMachineControllable.ChangeThisPositionReferencePointTransform(_spellAimPoint);
            StateMachineControllable.StartKeepingCurrentTargetOnDistance(_useSpellsStateData.DataForMoving);

            SubscribeOnLocalEvents();

            TryCastSelectedSpell();
        }

        protected override void SpecialExitAction()
        {
            UnsubscribeFromLocalEvents();
            StateMachineControllable.ChangeThisPositionReferencePointTransform(
                _previousThisPositionReferencePointTransform);
            StateMachineControllable.StopMoving();
            _isWaitingForAnimationFinish = false;
        }

        protected override void Awake()
        {
            base.Awake();
            _currentLookPointCalculator = new FollowTargetLookPointCalculator();
            _spellsSelector = _useSpellsStateData.GetSpellSelector(this);
            _spellSpawnObject = _spellSpawnObjectReadonlyTransformGetter.ReadonlyTransform;
            _spellAimPoint = _spellAimPointReadonlyTransformGetter.ReadonlyTransform;
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _spellsSelector.CanUseSpellsAgain += OnCanUseSpellsAgain;

            if (IsActivated)
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

        protected override void ChangeLookPointCalculator(ILookPointCalculator newCalculator)
        {
            _currentLookPointCalculator = newCalculator;
            base.ChangeLookPointCalculator(_currentLookPointCalculator);
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
            if (IsActivated)
            {
                HandleCompletedAction();
            }
            else
            {
                throw new InvalidOperationException();
            }

            if (!_spellsSelector.CanUseSpell)
            {
                _currentLookPointCalculator = new FollowTargetLookPointCalculator();
                ChangeLookPointCalculator(_currentLookPointCalculator);
            }

            _isWaitingForAnimationFinish = false;
            TryCastSelectedSpell();
        }

        private void OnActionAnimationKeyMomentTrigger()
        {
            CreateSelectedSpell();
        }

        private void OnCanUseSpellsAgain()
        {
            CanUseSpellsAgain?.Invoke();
            if (IsActivated)
            {
                TryCastSelectedSpell();
            }
        }

        private void TryCastSelectedSpell()
        {
            if (_isWaitingForAnimationFinish || !_spellsSelector.CanUseSpell ||
                (_wasSpellCasted && NeedTransitAfterAction)) return;

            _wasSpellCasted = true;
            _isWaitingForAnimationFinish = true;
            _cachedSpell = _spellsSelector.Pop();
            ChangeLookPointCalculator(_cachedSpell.LookPointCalculator);
            StateMachineControllable.PlayActionAnimation(_cachedSpell.SpellAnimationData);
        }

        private void CreateSelectedSpell()
        {
            _spellObjectsFactory.Create(_cachedSpell.SpellDataForSpellController,
                _cachedSpell.SpellPrefabProvider, _enemyController, _spellSpawnObject.Position, SpellSpawnDirection);
        }
    }
}