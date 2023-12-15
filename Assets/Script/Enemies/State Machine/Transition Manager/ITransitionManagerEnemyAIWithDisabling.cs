using System;
using Common.Abstract_Bases.Disableable;
using Enemies.State_Machine.States;

namespace Enemies.State_Machine.Transition_Manager
{
    public interface ITransitionManagerEnemyAIWithDisabling : IDisableable
    {
        public event Action<IStateEnemyAI> NeedTransit;
        public void StartCheckingConditions();
        public void StopCheckingConditions();
        public bool TryTransit(out IStateEnemyAI stateEnemyAI);
    }
}