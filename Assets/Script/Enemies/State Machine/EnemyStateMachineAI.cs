using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Enemies.Look_Point_Calculator;
using Enemies.State_Machine.States;
using Enemies.Target_Selector_From_Triggers;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine
{
    public class EnemyStateMachineAI : InitializableMonoBehaviourBase, IEnemyStateMachineAI,
        IInitializableEnemyStateMachineAI
    {
        [SerializeField] private StateEnemyAI _firstStateEnemyAI;
        private IStateEnemyAI _currentStateEnemyAI;
        private IEnemyStateMachineControllable _stateMachineControllable;
        private bool _isActive;

        public void Initialize(IEnemyStateMachineControllable stateMachineControllable)
        {
            _stateMachineControllable = stateMachineControllable;
            SetInitializedStatus();
        }

        public event Action<ILookPointCalculator> NeedChangeLookPointCalculator;

        private IEnemyTargetFromTriggersSelector TargetFromTriggersSelector =>
            _stateMachineControllable.TargetFromTriggersSelector;

        public void StartStateMachine()
        {
            if (_isActive)
            {
                throw new StateMachineAlreadyStartedException();
            }
            
            _isActive = true;
            TransitToState(_firstStateEnemyAI);
        }

        public void StopStateMachine()
        {
            if (!_isActive)
            {
                throw new StateMachineAlreadyStoppedException();
            }
            
            _isActive = false;
            TransitToState(null);
        }

        private void TransitToState(IStateEnemyAI nextStateEnemyAI)
        {
            if (_currentStateEnemyAI != null)
            {
                UnsubscribeFromCurrentStateEvents();
                _currentStateEnemyAI.Exit();
            }

            _currentStateEnemyAI = nextStateEnemyAI;

            if (_currentStateEnemyAI != null)
            {
                NeedChangeLookPointCalculator?.Invoke(_currentStateEnemyAI.LookPointCalculator);
                SubscribeOnCurrentStateEvents();
                _currentStateEnemyAI.Enter();
            }
        }

        private void OnCurrentTargetFromTriggersChanged(IEnemyTarget obj)
        {
            TransitToState(_currentStateEnemyAI);
        }

        protected override void SubscribeOnEvents()
        {
            if (!_isActive) return;
            SubscribeOnCurrentStateEvents();
        }
        
        protected override void UnsubscribeFromEvents()
        {
            UnsubscribeFromCurrentStateEvents();
        }

        private void SubscribeOnCurrentStateEvents()
        {
            TargetFromTriggersSelector.CurrentTargetChanged += OnCurrentTargetFromTriggersChanged;
            if (_currentStateEnemyAI == null) return;
            _currentStateEnemyAI.NeedToSwitchToNextState += TransitToState;
        }

        private void UnsubscribeFromCurrentStateEvents()
        {
            TargetFromTriggersSelector.CurrentTargetChanged -= OnCurrentTargetFromTriggersChanged;
            if (_currentStateEnemyAI == null) return;
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