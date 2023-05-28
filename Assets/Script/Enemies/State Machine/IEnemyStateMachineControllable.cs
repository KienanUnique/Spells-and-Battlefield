using Enemies.Target_Selector;
using Enemies.Trigger;
using UnityEngine;

namespace Enemies.State_Machine
{
    public interface IEnemyStateMachineControllable
    {
        public IEnemyTargetSelector TargetSelector { get; }
        public void StartMovingToTarget(Transform target);
        public void StopMovingToTarget();
    }
}