using System;

namespace Enemies.State_Machine
{
    public interface ITransitionEnemyAI
    {
        public event Action<IStateEnemyAI> NeedTransit;
        public void StartCheckingConditions(IEnemyStateMachineControllable stateMachineControllable);
        public void StopCheckingConditions();
    }
}