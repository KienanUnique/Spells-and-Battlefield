using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Enemies.Look_Point_Calculator;
using Enemies.State_Machine.Transitions;
using UnityEngine;

namespace Enemies.State_Machine.States
{
    public abstract class StateEnemyAI : InitializableMonoBehaviourBase, IStateEnemyAI, IInitializableStateEnemyAI
    {
        [SerializeField] private List<TransitionEnemyAI> _transitions;
        private bool _isActivated = false;

        public void Initialize(IEnemyStateMachineControllable stateMachineControllable)
        {
            StateMachineControllable = stateMachineControllable;
            SetInitializedStatus();
        }

        public event Action<IStateEnemyAI> NeedToSwitchToNextState;
        public abstract event Action<ILookPointCalculator> NeedChangeLookPointCalculator;

        public abstract ILookPointCalculator LookPointCalculator { get; }
        protected IEnemyStateMachineControllable StateMachineControllable { get; private set; }
        protected bool IsActivated => _isActivated;

        public void Enter()
        {
            if (_isActivated)
            {
                throw new StateIsAlreadyActivatedException();
            }

            _isActivated = true;

            SpecialEnterAction();

            SubscribeOnTransitionEvents();

            foreach (var transition in _transitions)
            {
                transition.StartCheckingConditions();
            }
        }

        public void Exit()
        {
            if (!_isActivated)
            {
                throw new TryingDeactivateNotActivatedStateException();
            }

            foreach (var transition in _transitions)
            {
                transition.StopCheckingConditions();
            }

            SpecialExitAction();

            _isActivated = false;
        }

        protected abstract void SpecialEnterAction();
        protected abstract void SpecialExitAction();

        protected override void SubscribeOnEvents()
        {
            if (!_isActivated) return;
            SubscribeOnTransitionEvents();
        }


        protected override void UnsubscribeFromEvents()
        {
            UnsubscribeFromTransitionEvents();
        }

        private void SubscribeOnTransitionEvents()
        {
            foreach (var transition in _transitions)
            {
                transition.NeedTransit += OnNeedTransit;
            }
        }

        private void UnsubscribeFromTransitionEvents()
        {
            foreach (var transition in _transitions)
            {
                transition.NeedTransit -= OnNeedTransit;
            }
        }

        private void OnNeedTransit(IStateEnemyAI nextState)
        {
            UnsubscribeFromTransitionEvents();
            NeedToSwitchToNextState?.Invoke(nextState);
        }

        private class StateIsAlreadyActivatedException : Exception
        {
            public StateIsAlreadyActivatedException() : base("State is already activated")
            {
            }
        }

        private class TryingDeactivateNotActivatedStateException : Exception
        {
            public TryingDeactivateNotActivatedStateException() : base("Can't deactivate not active state")
            {
            }
        }
    }
}