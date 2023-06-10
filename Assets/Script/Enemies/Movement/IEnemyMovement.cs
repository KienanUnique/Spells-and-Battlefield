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
        public event Action<bool> MovingStateChanged;
        public Vector3 CurrentPosition { get; }
        public void StartFollowingPosition(IReadonlyTransform target);
        public void StopMovingToTarget();
        public void AddForce(Vector3 force, ForceMode mode);
    }
}