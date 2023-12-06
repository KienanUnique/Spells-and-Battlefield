using System;
using Common.Animation_Data;
using Common.Animation_Data.Continuous_Action;
using Common.Interfaces;
using Common.Readonly_Transform;
using Enemies.Controller;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spell_Selectors;
using Enemies.State_Machine.States.Concrete_Types.Use_Spells.Spells_Manager;
using Spells.Factory;
using Spells.Spell_Handlers.Continuous;
using UnityEngine;
using Zenject;

namespace Enemies.State_Machine.States.Concrete_Types.Use_Spells
{
    public class UseSpellsStateEnemyAI : StateEnemyAI, ICoroutineStarter, IReadonlySpellSelector
    {
        [SerializeField] private UseSpellsStateData _useSpellsStateData;
        [SerializeField] private EnemyController _enemyController;
        [SerializeField] private ReadonlyTransformGetter _spellSpawnObjectReadonlyTransformGetter;
        private IEnemySpellsManager _spellsManager;
        private ILookPointCalculator _currentLookPointCalculator;
        private IReadonlyTransform _previousThisPositionReferencePointTransform;
        private ISpellObjectsFactory _spellObjectsFactory;
        private IReadonlyTransform _spellSpawnPoint;
        private Coroutine _tryUseSpellCoroutine;

        [Inject]
        private void GetDependencies(ISpellObjectsFactory spellObjectsFactory)
        {
            _spellObjectsFactory = spellObjectsFactory;
        }

        public event Action CanUseSpellsAgain;
        public override ILookPointCalculator LookPointCalculator => _currentLookPointCalculator;
        public bool CanUseSpell => _spellsManager.CanUseSpell;

        protected override void SpecialInitializeAction()
        {
            base.SpecialInitializeAction();
            _currentLookPointCalculator = new FollowTargetLookPointCalculator();
            _spellSpawnPoint = _spellSpawnObjectReadonlyTransformGetter.ReadonlyTransform;
            IEnemySpellSelector spellsSelector = _useSpellsStateData.GetSpellSelector(this);
            var continuousSpellHandlerImplementation = new ContinuousSpellHandlerImplementation(_enemyController,
                _spellObjectsFactory, _spellSpawnPoint);
            var instantSpellHandlerImplementation = new EnemyInstantSpellHandlerImplementation(_enemyController,
                _spellObjectsFactory, _spellSpawnPoint, StateMachineControllable);
            _spellsManager = new EnemySpellsManager(continuousSpellHandlerImplementation,
                instantSpellHandlerImplementation, spellsSelector, _currentLookPointCalculator);
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _spellsManager.CanUseSpellsAgain += OnCanUseSpellsAgain;

            if (CurrentStatus == StateEnemyAIStatus.Active)
            {
                SubscribeOnLocalEvents();
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            _spellsManager.CanUseSpellsAgain -= OnCanUseSpellsAgain;
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
                    break;

                case StateEnemyAIStatus.Active:
                    _previousThisPositionReferencePointTransform =
                        StateMachineControllable.ThisPositionReferencePointForLook;
                    StateMachineControllable.ChangeThisPositionReferencePointTransform(_spellSpawnPoint);
                    StateMachineControllable.StartKeepingCurrentTargetOnDistance(_useSpellsStateData.DataForMoving);
                    SubscribeOnLocalEvents();
                    _spellsManager.StartCasting();
                    break;

                case StateEnemyAIStatus.Exiting:
                    _spellsManager.StopCasting();
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

        private void SubscribeOnLocalEvents()
        {
            _spellsManager.Enable();
            _spellsManager.NeedChangeLookPointCalculator += ChangeLookPointCalculator;
            _spellsManager.FinishedCasting += OnFinishedCasting;
            _spellsManager.NeedCancelActionAnimations += OnNeedCancelActionAnimations;
            _spellsManager.NeedPlaySingleActionAnimation += OnNeedPlaySingleActionAnimation;
            _spellsManager.NeedPlayContinuousActionAnimation += OnNeedPlayContinuousActionAnimation;
            StateMachineControllable.ActionAnimationEnd += _spellsManager.OnAnimatorReadyForNextAnimation;
            StateMachineControllable.ActionAnimationKeyMomentTrigger +=
                _spellsManager.OnSpellCastPartOfAnimationFinished;
        }

        private void UnsubscribeFromLocalEvents()
        {
            _spellsManager.Disable();
            _spellsManager.NeedChangeLookPointCalculator -= ChangeLookPointCalculator;
            _spellsManager.FinishedCasting -= OnFinishedCasting;
            _spellsManager.NeedCancelActionAnimations -= OnNeedCancelActionAnimations;
            _spellsManager.NeedPlaySingleActionAnimation -= OnNeedPlaySingleActionAnimation;
            _spellsManager.NeedPlayContinuousActionAnimation -= OnNeedPlayContinuousActionAnimation;
            StateMachineControllable.ActionAnimationEnd -= _spellsManager.OnAnimatorReadyForNextAnimation;
            StateMachineControllable.ActionAnimationKeyMomentTrigger -=
                _spellsManager.OnSpellCastPartOfAnimationFinished;
        }

        private void OnNeedPlayContinuousActionAnimation(IContinuousActionAnimationData obj)
        {
            StateMachineControllable.PlayActionAnimation(obj);
        }

        private void OnNeedPlaySingleActionAnimation(IAnimationData obj)
        {
            StateMachineControllable.PlayActionAnimation(obj);
        }

        private void OnNeedCancelActionAnimations()
        {
            StateMachineControllable.CancelActionAnimation();
        }

        private void OnFinishedCasting()
        {
            HandleExitFromState();
        }

        private void OnCanUseSpellsAgain()
        {
            CanUseSpellsAgain?.Invoke();
        }
    }
}