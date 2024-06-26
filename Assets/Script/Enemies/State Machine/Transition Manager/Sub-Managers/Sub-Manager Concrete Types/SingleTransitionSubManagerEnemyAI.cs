﻿using System;
using Enemies.State_Machine.States;
using Enemies.State_Machine.Transition_Conditions;
using UnityEngine;

namespace Enemies.State_Machine.Transition_Manager.Sub_Managers.Sub_Manager_Concrete_Types
{
    [Serializable]
    public class SingleTransitionSubManagerEnemyAI : TransitionSubManagerEnemyAIBase
    {
        [SerializeField] private TransitionConditionEnemyAIBase _condition;

        public override bool TryTransit(out IStateEnemyAI stateEnemyAI)
        {
            if (_condition.IsConditionCompleted)
            {
                stateEnemyAI = NextState;
                return true;
            }

            stateEnemyAI = null;
            return false;
        }

        protected override void HandleStartCheckingConditions()
        {
            _condition.StartCheckingConditions();
        }

        protected override void HandleStopCheckingConditions()
        {
            _condition.StopCheckingConditions();
        }

        protected override void SubscribeOnTransitionEvents()
        {
            _condition.ConditionCompleted += OnConditionCompleted;
        }

        protected override void UnsubscribeFromTransitionEvents()
        {
            _condition.ConditionCompleted -= OnConditionCompleted;
        }

        private void OnConditionCompleted()
        {
            InvokeNeedTransitEvent();
        }
    }
}