using System;
using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Disableable;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Animator_Status_Controller;
using DG.Tweening;
using Enemies.Look_Point_Calculator;
using Enemies.State_Machine.Transition_Manager;
using UnityEngine;

namespace Enemies.State_Machine.States
{
    public abstract class StateEnemyAI : InitializableMonoBehaviourBase, IStateEnemyAI, IInitializableStateEnemyAI
    {
        private const float CheckTransitionsDelay = 0.2f;

        [SerializeField] private MainTransitionManagerEnemyAI _transitionManager;
        private IStateEnemyAI _cachedNextState;
        private ValueWithReactionOnChange<StateEnemyAIStatus> _currentStateStatus;
        private Tween _startCheckingTransitionsTween;
        private bool _canInstantCheck = true;

        public void Initialize(IEnemyStateMachineControllable stateMachineControllable,
            IReadonlyAnimatorStatusChecker animatorStatusChecker)
        {
            StateMachineControllable = stateMachineControllable;
            AnimatorStatusChecker = animatorStatusChecker;
            _currentStateStatus = new ValueWithReactionOnChange<StateEnemyAIStatus>(StateEnemyAIStatus.NonActive);
            SetItemsNeedDisabling(new List<IDisableable> {_transitionManager});
            SpecialInitializeAction();
            SetInitializedStatus();
        }

        public event Action<IStateEnemyAI> NeedToSwitchToNextState;
        public event Action<ILookPointCalculator> NeedChangeLookPointCalculator;

        protected enum StateEnemyAIStatus
        {
            NonActive, Active, Exiting
        }

        public abstract ILookPointCalculator LookPointCalculator { get; }
        public int StateID => GetInstanceID();
        protected IEnemyStateMachineControllable StateMachineControllable { get; private set; }

        protected IReadonlyAnimatorStatusChecker AnimatorStatusChecker { get; private set; }

        protected StateEnemyAIStatus CurrentStatus => _currentStateStatus.Value;

        public void Enter()
        {
            _currentStateStatus.Value = StateEnemyAIStatus.Active;
        }

        public void Exit()
        {
            _currentStateStatus.Value = StateEnemyAIStatus.NonActive;
        }

        public void ExitSafely(IStateEnemyAI nextState)
        {
            PrepareToExit(nextState);
        }

        protected abstract void SpecialReactionOnStateStatusChange(StateEnemyAIStatus newStatus);

        protected virtual void SpecialInitializeAction()
        {
        }

        protected virtual void ChangeLookPointCalculator(ILookPointCalculator lookPointCalculator)
        {
            NeedChangeLookPointCalculator?.Invoke(lookPointCalculator);
        }

        protected override void SubscribeOnEvents()
        {
            _currentStateStatus.AfterValueChanged += OnAfterStateStatusChanged;
            if (CurrentStatus == StateEnemyAIStatus.Active)
            {
                SubscribeOnTransitionEvents();
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            _currentStateStatus.AfterValueChanged -= OnAfterStateStatusChanged;
            UnsubscribeFromTransitionEvents();
        }

        protected void HandleCompletedAction()
        {
            _transitionManager.HandleCompletedAction();
        }

        protected void HandleExitFromState()
        {
            NeedToSwitchToNextState?.Invoke(_cachedNextState);
        }

        private void OnAfterStateStatusChanged(StateEnemyAIStatus newStatus)
        {
            switch (newStatus)
            {
                case StateEnemyAIStatus.Active:
                    _transitionManager.StartCheckingConditions();
                    if (_canInstantCheck)
                    {
                        if (_transitionManager.TryTransit(out var nextState))
                        {
                            
                            _canInstantCheck = false;
                            _startCheckingTransitionsTween?.Kill();
                            _startCheckingTransitionsTween = DOVirtual
                                                             .DelayedCall(CheckTransitionsDelay, StartCheckingTransitions)
                                                             .SetLink(gameObject);
                            _cachedNextState = nextState;
                            HandleExitFromState();
                            return;
                        }
                    }
                    else
                    {
                        _startCheckingTransitionsTween?.OnComplete()
                    }
                    
                    SubscribeOnTransitionEvents();
                    break;
                case StateEnemyAIStatus.Exiting:
                    UnsubscribeFromTransitionEvents();
                    _transitionManager.StopCheckingConditions();
                    break;
                case StateEnemyAIStatus.NonActive:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
            
            SpecialReactionOnStateStatusChange(_currentStateStatus.Value);
        }

        private void StartCheckingTransitions()
        {
            _canInstantCheck = true;
            
        }

        private void SubscribeOnTransitionEvents()
        {
            _transitionManager.NeedTransit += OnNeedTransit;
        }

        private void UnsubscribeFromTransitionEvents()
        {
            _transitionManager.NeedTransit -= OnNeedTransit;
        }

        private void OnNeedTransit(IStateEnemyAI nextState)
        {
            PrepareToExit(nextState);
        }

        private void PrepareToExit(IStateEnemyAI nextState)
        {
            if (_currentStateStatus.Value != StateEnemyAIStatus.Active)
            {
                return;
            }

            _cachedNextState = nextState;
            _currentStateStatus.Value = StateEnemyAIStatus.Exiting;
        }
    }
}