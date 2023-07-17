using System;
using Enemies.Look;
using Enemies.Target_Selector_From_Triggers;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine
{
    public class EnemyStateMachineAI : MonoBehaviour, IEnemyStateMachineAI
    {
        [SerializeField] private StateEnemyAI _firstStateEnemyAI;
        private IStateEnemyAI _currentStateEnemyAI;
        private IEnemyStateMachineControllable _stateMachineControllable;
        private IEnemyLook _enemyLook;
        private bool _isActive;
        private IEnemyTargetFromTriggersSelector _targetFromTriggersSelector;

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

        public void StartStateMachine(IEnemyStateMachineControllable stateMachineControllable, IEnemyLook enemyLook)
        {
            if (_isActive)
            {
                throw new StateMachineAlreadyStartedException();
            }

            _enemyLook = enemyLook;
            _stateMachineControllable = stateMachineControllable;
            _targetFromTriggersSelector = _stateMachineControllable.TargetFromTriggersSelector;
            _targetFromTriggersSelector.CurrentTargetChanged += OnCurrentTargetFromTriggersChanged;
            _enemyLook.StartLooking();
            TransitToState(_firstStateEnemyAI);
            _isActive = true;
        }

        public void StopStateMachine()
        {
            if (!_isActive)
            {
                throw new StateMachineAlreadyStoppedException();
            }

            _enemyLook.StopLooking();
            TransitToState(null);
            _isActive = false;
        }

        private void TransitToState(IStateEnemyAI nextStateEnemyAI)
        {
            if (_currentStateEnemyAI != null)
            {
                UnsubscribeFromEvents();
                _currentStateEnemyAI.Exit();
            }

            _currentStateEnemyAI = nextStateEnemyAI;

            if (_currentStateEnemyAI != null)
            {
                _enemyLook.SetLookPointCalculator(_currentStateEnemyAI.LookPointCalculator);
                SubscribeOnEvents();
                _currentStateEnemyAI.Enter(_stateMachineControllable);
            }
        }

        private void OnCurrentTargetFromTriggersChanged(IEnemyTarget obj)
        {
            TransitToState(_currentStateEnemyAI);
        }

        private void SubscribeOnEvents()
        {
            _targetFromTriggersSelector.CurrentTargetChanged += OnCurrentTargetFromTriggersChanged;
            _currentStateEnemyAI.NeedToSwitchToNextState += TransitToState;
        }


        private void UnsubscribeFromEvents()
        {
            _targetFromTriggersSelector.CurrentTargetChanged -= OnCurrentTargetFromTriggersChanged;
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