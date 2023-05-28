using System;
using Enemies.Target_Selector;
using Enemies.Trigger;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine
{
    public class EnemyStateMachineAI : MonoBehaviour
    {
        [SerializeField] private StateEnemyAI _firstStateEnemyAI;
        private StateEnemyAI _currentStateEnemyAI;
        private IEnemyStateMachineControllable _stateMachineControllable;
        private bool _isActive;
        private IEnemyTargetSelector _targetSelector;

        private void OnEnable()
        {
            if (_isActive)
            {
                SubscribeOnEvents();
            }
        }

        private void OnDisable()
        {
            if (_isActive)
            {
                UnsubscribeFromEvents();
            }
        }

        public void StartStateMachine(IEnemyStateMachineControllable stateMachineControllable)
        {
            if (_isActive)
            {
                throw new StateMachineAlreadyStartedException();
            }

            _stateMachineControllable = stateMachineControllable;
            _targetSelector = _stateMachineControllable.TargetSelector;
            _targetSelector.CurrentTargetChanged += OnCurrentTargetChanged;
            TransitToState(_firstStateEnemyAI);
            _isActive = true;
        }

        public void StopStateMachine()
        {
            if (!_isActive)
            {
                throw new StateMachineAlreadyStoppedException();
            }

            TransitToState(null);
            _isActive = false;
        }

        private void TransitToState(StateEnemyAI nextStateEnemyAI)
        {
            if (_currentStateEnemyAI != null)
            {
                UnsubscribeFromEvents();
                _currentStateEnemyAI.Exit();
            }

            _currentStateEnemyAI = nextStateEnemyAI;

            if (_currentStateEnemyAI != null)
            {
                SubscribeOnEvents();
                _currentStateEnemyAI.Enter(_stateMachineControllable);
            }
        }

        private void OnCurrentTargetChanged(IEnemyTarget obj)
        {
            TransitToState(_currentStateEnemyAI);
        }

        private void SubscribeOnEvents()
        {
            _targetSelector.CurrentTargetChanged += OnCurrentTargetChanged;
            _currentStateEnemyAI.NeedToSwitchToNextState += TransitToState;
        }


        private void UnsubscribeFromEvents()
        {
            _targetSelector.CurrentTargetChanged -= OnCurrentTargetChanged;
            _currentStateEnemyAI.NeedToSwitchToNextState -= TransitToState;
        }

        private class StateMachineAlreadyStartedException : Exception
        {
            public StateMachineAlreadyStartedException() : base("State machine already started")
            {
            }
        }

        private class StateMachineAlreadyStoppedException : Exception
        {
            public StateMachineAlreadyStoppedException() : base("State machine already stopped")
            {
            }
        }
    }
}