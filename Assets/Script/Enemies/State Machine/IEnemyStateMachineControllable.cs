using Enemies.Target_Selector_From_Triggers;
using UnityEngine;

namespace Enemies.State_Machine
{
    public interface IEnemyStateMachineControllable
    {
        public IEnemyTargetFromTriggersSelector TargetFromTriggersSelector { get; }
        public void StartMovingToTarget(Transform target);
        public void StopMovingToTarget();
    }
}