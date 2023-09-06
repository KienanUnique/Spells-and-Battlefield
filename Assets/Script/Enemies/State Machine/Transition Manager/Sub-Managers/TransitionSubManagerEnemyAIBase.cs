using System;
using Enemies.State_Machine.States;
using UnityEngine;

namespace Enemies.State_Machine.Transition_Manager.Sub_Managers
{
    [Serializable]
    public abstract class TransitionSubManagerEnemyAIBase : TransitionManagerEnemyAIBase
    {
        [SerializeField] private StateEnemyAI _nextState;

        public override event Action<IStateEnemyAI> NeedTransit;

        protected void InvokeNeedTransitEvent()
        {
            NeedTransit?.Invoke(_nextState);
        }
    }
}