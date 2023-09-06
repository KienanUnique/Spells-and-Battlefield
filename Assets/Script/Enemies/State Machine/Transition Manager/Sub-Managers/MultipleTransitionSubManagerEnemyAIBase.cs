using System;
using System.Collections.Generic;
using Enemies.State_Machine.Transition_Conditions;
using UnityEngine;

namespace Enemies.State_Machine.Transition_Manager.Sub_Managers
{
    [Serializable]
    public abstract class MultipleTransitionSubManagerEnemyAIBase : TransitionSubManagerEnemyAIBase
    {
        [SerializeField] protected List<TransitionConditionEnemyAIBase> _conditions;

        protected abstract bool IsNeedConditionsCompleted();

        protected override void HandleStartCheckingConditions()
        {
            foreach (TransitionConditionEnemyAIBase condition in _conditions)
            {
                condition.StartCheckingConditions();
            }
        }

        protected override void HandleStopCheckingConditions()
        {
            foreach (TransitionConditionEnemyAIBase condition in _conditions)
            {
                condition.StopCheckingConditions();
            }
        }

        protected override void SubscribeOnTransitionEvents()
        {
            foreach (TransitionConditionEnemyAIBase condition in _conditions)
            {
                condition.ConditionCompleted += OnConditionCompleted;
            }
        }

        protected override void UnsubscribeFromTransitionEvents()
        {
            foreach (TransitionConditionEnemyAIBase condition in _conditions)
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