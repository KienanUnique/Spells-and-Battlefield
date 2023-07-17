﻿using System;
using System.Collections.Generic;
using Enemies.Look_Point_Calculator;
using UnityEngine;

namespace Enemies.State_Machine
{
    public abstract class StateEnemyAI : MonoBehaviour, IStateEnemyAI
    {
        [SerializeField] private List<TransitionEnemyAI> _transitions;
        private bool _isActivated = false;

        public event Action<IStateEnemyAI> NeedToSwitchToNextState;

        protected IEnemyStateMachineControllable StateMachineControllable { get; private set; }

        public abstract ILookPointCalculator LookPointCalculator { get; }

        public void Enter(IEnemyStateMachineControllable stateMachineControllable)
        {
            if (_isActivated)
            {
                throw new StateIsAlreadyActivatedException();
            }

            StateMachineControllable = stateMachineControllable;
            _isActivated = true;

            SpecialEnterAction();

            SubscribeOnTransitionEvents();

            foreach (var transition in _transitions)
            {
                transition.StartCheckingConditions(StateMachineControllable);
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

        private void OnEnable()
        {
            if (_isActivated)
            {
                SubscribeOnTransitionEvents();
            }
        }

        private void OnDisable()
        {
            if (_isActivated)
            {
                UnsubscribeFromTransitionEvents();
            }
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