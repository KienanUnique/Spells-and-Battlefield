using System;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine.Transition_Conditions.Concrete_Types
{
    public class TargetsCountChangedTransitionConditionEnemyAI : TransitionConditionEnemyAIBase
    {
        [SerializeField] private TargetsCount _requiredTargetsCount;

        private enum TargetsCount
        {
            NoTargetsLeft, HaveTargets
        }

        public override bool IsConditionCompleted
        {
            get
            {
                return _requiredTargetsCount switch
                {
                    TargetsCount.NoTargetsLeft => CurrentTarget == null,
                    TargetsCount.HaveTargets => CurrentTarget != null,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        private IEnemyTarget CurrentTarget => StateMachineControllable.TargetFromTriggersSelector.CurrentTarget;

        protected override void HandleStartCheckingConditions()
        {
            SubscribeOnTargetSelectorEvents();
        }

        protected override void HandleStopCheckingConditions()
        {
            UnsubscribeFromTargetSelectorEvents();
        }

        protected override void SubscribeOnEvents()
        {
            if (!IsEnabled)
            {
                return;
            }

            SubscribeOnTargetSelectorEvents();
        }

        protected override void UnsubscribeFromEvents()
        {
            UnsubscribeFromTargetSelectorEvents();
        }

        private void SubscribeOnTargetSelectorEvents()
        {
            StateMachineControllable.TargetFromTriggersSelector.CurrentTargetChanged += OnCurrentTargetChanged;
        }

        private void UnsubscribeFromTargetSelectorEvents()
        {
            StateMachineControllable.TargetFromTriggersSelector.CurrentTargetChanged -= OnCurrentTargetChanged;
        }

        private void OnCurrentTargetChanged(IEnemyTarget oldTarget, IEnemyTarget newTarget)
        {
            if (IsConditionCompleted)
            {
                InvokeConditionCompletedEvent();
            }
        }
    }
}