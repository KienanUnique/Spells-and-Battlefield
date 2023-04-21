using System;
using UnityEngine;

namespace Enemies.State_Machine
{
    public class EnemyStateMachineAI : MonoBehaviour
    {
        [SerializeField] private StateEnemyAI _firstStateEnemyAI;
        private StateEnemyAI _currentStateEnemyAI;
        private IEnemyStateMachineControllable _stateMachineControllable;
        private bool _isActive = false;

        private void OnEnable()
        {
            if (_isActive)
            {
                SubscribeOnCurrentStateEvents();
            }
        }

        private void OnDisable()
        {
            if (_isActive)
            {
                UnsubscribeFromCurrentStateEvents();
            }
        }

        public void StartStateMachine(IEnemyStateMachineControllable stateMachineControllable)
        {
            if (_isActive)
            {
                throw new StateMachineAlreadyStartedException();
            }

            _stateMachineControllable = stateMachineControllable;
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
                UnsubscribeFromCurrentStateEvents();
                _currentStateEnemyAI.Exit();
            }

            _currentStateEnemyAI = nextStateEnemyAI;

            if (_currentStateEnemyAI != null)
            {
                SubscribeOnCurrentStateEvents();
                _currentStateEnemyAI.Enter(_stateMachineControllable);
            }
        }

        private void SubscribeOnCurrentStateEvents()
        {
            _currentStateEnemyAI.NeedToSwitchToNextState += TransitToState;
        }

        private void UnsubscribeFromCurrentStateEvents()
        {
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