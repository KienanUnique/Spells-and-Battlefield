using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine
{
    public interface IEnemyStateMachineControllable
    {
        public IEnemyTarget Target { get; }
        public void StartMovingToTarget(Transform target);
        public void StopMovingToTarget();
    }
}