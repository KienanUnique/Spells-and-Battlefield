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
            SubscribeOnTargetSelectorEvents();
            TransitToState(_firstStateEnemyAI);
        }

        public void StopStateMachine()
        {
            if (!_isActive)
            {
                throw new StateMachineAlreadyStoppedException();
            }

            _isActive = false;
            UnsubscribeFromTargetSelectorEvents();
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

        private void OnCurrentTargetFromTriggersChanged(IEnemyTarget oldTarget, IEnemyTarget newTarget)
        {
            if (newTarget != null)
            {
                TransitToState(_currentStateEnemyAI ?? _firstStateEnemyAI);
            }
            else
            {
                TransitToState(null);
            }
        }

        protected override void SubscribeOnEvents()
        {
            if (!_isActive) return;
            SubscribeOnTargetSelectorEvents();
            SubscribeOnCurrentStateEvents();
        }

        protected override void UnsubscribeFromEvents()
        {
            UnsubscribeFromTargetSelectorEvents();
            UnsubscribeFromCurrentStateEvents();
        }

        private void SubscribeOnTargetSelectorEvents()
        {
            TargetFromTriggersSelector.CurrentTargetChanged += OnCurrentTargetFromTriggersChanged;
        }

        private void UnsubscribeFromTargetSelectorEvents()
        {
            TargetFromTriggersSelector.CurrentTargetChanged -= OnCurrentTargetFromTriggersChanged;
        }

        private void SubscribeOnCurrentStateEvents()
        {
            if (_currentStateEnemyAI == null) return;
            _currentStateEnemyAI.NeedToSwitchToNextState += TransitToState;
            _currentStateEnemyAI.NeedChangeLookPointCalculator += OnNeedChangeLookPointCalculator;
        }

        private void UnsubscribeFromCurrentStateEvents()
        {
            if (_currentStateEnemyAI == null) return;
            _currentStateEnemyAI.NeedToSwitchToNextState -= TransitToState;
            _currentStateEnemyAI.NeedChangeLookPointCalculator -= OnNeedChangeLookPointCalculator;
        }

        private void OnNeedChangeLookPointCalculator(ILookPointCalculator newLookPointCalculator)
        {
            NeedChangeLookPointCalculator?.Invoke(newLookPointCalculator);
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