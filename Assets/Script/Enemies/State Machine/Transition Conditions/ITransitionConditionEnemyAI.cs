using System;

namespace Enemies.State_Machine.Transition_Conditions
{
    public interface ITransitionConditionEnemyAI
    {
        public event Action ConditionCompleted;
        public bool IsConditionCompleted { get; }
        public void StartCheckingConditions();
        public void StopCheckingConditions();
    }
}