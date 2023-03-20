using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine
{
    public interface IEnemyStateMachineControllable : IEnemy
    {
        public IEnemyTarget Target { get; set; }
        public void StartMovingToTarget(Transform target);
        public void StopMovingToTarget();
    }
}