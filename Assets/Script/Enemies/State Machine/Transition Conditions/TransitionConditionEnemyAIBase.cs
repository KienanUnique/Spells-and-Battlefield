using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;

namespace Enemies.State_Machine.Transition_Conditions
{
    public abstract class TransitionConditionEnemyAIBase : InitializableMonoBehaviourBase, ITransitionConditionEnemyAI,
        IInitializableTransitionEnemyAI
    {
        public void Initialize(IEnemyStateMachineControllable stateMachineControllable)
        {
            StateMachineControllable = stateMachineControllable;
            SetInitializedStatus();
        }

        public event Action ConditionCompleted;

        protected IEnemyStateMachineControllable StateMachineControllable { get; private set; }
        protected bool IsEnabled { get; private set; }
        public abstract bool IsConditionCompleted { get; }

        public void StartCheckingConditions()
        {
            IsEnabled = true;
            HandleStartCheckingConditions();
            if (IsConditionCompleted)
            {
                InvokeConditionCompletedEvent();
            }
        }

        public void StopCheckingConditions()
        {
            IsEnabled = false;
            HandleStopCheckingConditions();
        }

        protected abstract void HandleStartCheckingConditions();
        protected abstract void HandleStopCheckingConditions();

        protected void InvokeConditionCompletedEvent()
        {
            ConditionCompleted?.Invoke();
        }
    }
}