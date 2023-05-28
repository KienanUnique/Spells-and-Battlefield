using System;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Movement;
using UnityEngine;

namespace Enemies.Movement
{
    public interface IEnemyMovement : IMovementBase
    {
        event Action<bool> MovingStateChanged;
        Vector3 CurrentPosition { get; }
        void StartMovingToTarget(Transform target);
        void StopMovingToTarget();
        void AddForce(Vector3 force, ForceMode mode);
    }
}