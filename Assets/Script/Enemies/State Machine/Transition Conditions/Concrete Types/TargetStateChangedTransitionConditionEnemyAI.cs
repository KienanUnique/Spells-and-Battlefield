using Common.Abstract_Bases.Character;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine.Transition_Conditions.Concrete_Types
{
    public class TargetStateChangedTransitionConditionEnemyAI : TransitionConditionEnemyAIBase
    {
        [SerializeField] private CharacterState _needState;
        private int _lastCashedTargetId;

        public override bool IsConditionCompleted =>
            CurrentTarget != null && CurrentTarget.CurrentCharacterState == _needState;

        private IEnemyTarget CurrentTarget => StateMachineControllable.TargetFromTriggersSelector.CurrentTarget;

        protected override void HandleStartCheckingConditions()
        {
            SubscribeOnCharacterEvents(CurrentTarget);
        }

        protected override void HandleStopCheckingConditions()
        {
            UnsubscribeFromCharacterEvents(CurrentTarget);
        }

        protected override void SubscribeOnEvents()
        {
            if (!IsEnabled)
            {
                return;
            }

            StateMachineControllable.TargetFromTriggersSelector.CurrentTargetChanged += OnCurrentTargetChanged;
            SubscribeOnCharacterEvents(CurrentTarget);
        }

        protected override void UnsubscribeFromEvents()
        {
            StateMachineControllable.TargetFromTriggersSelector.CurrentTargetChanged -= OnCurrentTargetChanged;
            UnsubscribeFromCharacterEvents(CurrentTarget);
        }

        private void SubscribeOnCharacterEvents(ICharacterInformationProvider target)
        {
            if (IsEnabled && CurrentTarget != null)
            {
                target.CharacterStateChanged += OnTargetCharacterStateChanged;
            }
        }

        private void UnsubscribeFromCharacterEvents(ICharacterInformationProvider target)
        {
            if (CurrentTarget != null)
            {
                target.CharacterStateChanged -= OnTargetCharacterStateChanged;
            }
        }

        private void OnTargetCharacterStateChanged(CharacterState newState)
        {
            if (newState == _needState)
            {
                InvokeConditionCompletedEvent();
            }
        }

        private void OnCurrentTargetChanged(IEnemyTarget oldTarget, IEnemyTarget newTarget)
        {
            if (oldTarget != null)
            {
                UnsubscribeFromCharacterEvents(oldTarget);
            }

            if (newTarget != null)
            {
                SubscribeOnCharacterEvents(newTarget);
            }
        }
    }
}