using System;
using System.Collections.Generic;
using Enemies.State_Machine.States;
using Enemies.State_Machine.Transition_Conditions;
using UnityEngine;

namespace Enemies.State_Machine.Transition_Manager.Sub_Managers
{
    [Serializable]
    public abstract class MultipleTransitionSubManagerEnemyAIBase : TransitionSubManagerEnemyAIBase
    {
        [SerializeField] protected List<TransitionConditionEnemyAIBase> _conditions;

        public override bool TryTransit(out IStateEnemyAI stateEnemyAI)
        {
            if (IsNeedConditionsCompleted())
            {
                stateEnemyAI = NextState;
                return true;
            }

            stateEnemyAI = null;
            return false;
        }

        protected abstract bool IsNeedConditionsCompleted();

        protected override void HandleStartCheckingConditions()
        {
            foreach (var condition in _conditions)
            {
                condition.StartCheckingConditions();
            }
        }

        protected override void HandleStopCheckingConditions()
        {
            foreach (var condition in _conditions)
            {
                condition.StopCheckingConditions();
            }
        }

        protected override void SubscribeOnTransitionEvents()
        {
            foreach (var condition in _conditions)
            {
                condition.ConditionCompleted += OnConditionCompleted;
            }
        }

        protected override void UnsubscribeFromTransitionEvents()
        {
            foreach (var condition in _conditions)
            {
                condition.ConditionCompleted -= OnConditionCompleted;
            }
        }

        private void OnConditionCompleted()
        {
            if (IsNeedConditionsCompleted())
            {
                InvokeNeedTransitEvent();
            }
        }
    }
}