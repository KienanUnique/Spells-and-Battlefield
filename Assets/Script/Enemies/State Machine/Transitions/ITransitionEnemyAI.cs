using System;
using Enemies.State_Machine.States;

namespace Enemies.State_Machine.Transitions
{
    public interface ITransitionEnemyAI
    {
        public event Action<IStateEnemyAI> NeedTransit;
        public void StartCheckingConditions();
        public void StopCheckingConditions();
    }
}