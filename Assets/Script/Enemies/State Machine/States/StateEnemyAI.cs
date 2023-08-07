using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Enemies.Look_Point_Calculator;
using Enemies.State_Machine.Transition_Manager;
using UnityEngine;

namespace Enemies.State_Machine.States
{
    public abstract class StateEnemyAI : InitializableMonoBehaviourBase, IStateEnemyAI, IInitializableStateEnemyAI
    {
        [SerializeField] private MainTransitionManagerEnemyAI _transitionManager;
        private bool _isActivated = false;

        public void Initialize(IEnemyStateMachineControllable stateMachineControllable)
        {
            StateMachineControllable = stateMachineControllable;
            SetItemsNeedDisabling(new List<IDisableable> {_transitionManager});
            SetInitializedStatus();
        }

        public event Action<IStateEnemyAI> NeedToSwitchToNextState;
        public event Action<ILookPointCalculator> NeedChangeLookPointCalculator;

        public int StateID => this.GetInstanceID();
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

            _transitionManager.StartCheckingConditions();
        }

        public void Exit()
        {
            if (!_isActivated)
            {
                throw new TryingDeactivateNotActivatedStateException();
            }

            _transitionManager.StopCheckingConditions();

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

        protected virtual void ChangeLookPointCalculator(ILookPointCalculator lookPointCalculator)
        {
            NeedChangeLookPointCalculator?.Invoke(lookPointCalculator);
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