using System;
using Common.Abstract_Bases.Movement;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using UnityEngine;

namespace Enemies.Movement
{
    public interface IEnemyMovement : IMovementBase, IEnemyMovementForStateMachine
    {
        public event Action<bool> MovingStateChanged;
        public Vector3 CurrentPosition { get; }
        public void DisableMoving();
        public void AddForce(Vector3 force, ForceMode mode);
        public void StickToPlatform(Transform platformTransform);
        public void UnstickFromPlatform();
    }

    public interface IEnemyMovementForStateMachine
    {
        public IReadonlyRigidbody ReadonlyRigidbody { get; }
        public void StartFollowingPosition(IReadonlyTransform target);
        public void StopMovingToTarget();
    }
}