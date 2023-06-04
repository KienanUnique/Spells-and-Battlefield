using System;
using Common;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Movement;
using Common.Readonly_Transform;
using UnityEngine;

namespace Enemies.Movement
{
    public interface IEnemyMovement : IMovementBase
    {
        event Action<bool> MovingStateChanged;
        Vector3 CurrentPosition { get; }
        void StartFollowingPosition(IReadonlyTransform target);
        void StopMovingToTarget();
        void AddForce(Vector3 force, ForceMode mode);
    }
}