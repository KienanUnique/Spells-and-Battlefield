using System;
using Common.Abstract_Bases.Movement;
using Common.Mechanic_Effects.Concrete_Types.Push;
using Common.Readonly_Rigidbody;
using UnityEngine;

namespace Player.Movement
{
    public interface IPlayerMovement : IMovementBase,
        IPlayerDashInformation,
        IPhysicsInteractable,
        IPlayerOverSpeedInformation
    {
        public event Action Land;
        public event Action GroundJump;
        public event Action AirJump;
        public event Action Fall;
        public event Action<WallDirection> StartWallRunning;
        public event Action<WallDirection> WallRunningDirectionChanged;
        public event Action EndWallRunning;
        public event Action HookingStarted;
        public event Action HookingEnded;
        public Vector2 NormalizedVelocityDirectionXY { get; }
        public float RatioOfCurrentVelocityToMaximumVelocity { get; }
        public Vector3 HookPoint { get; }
        public IReadonlyRigidbody MainRigidbody { get; }
        public void TryJumpInputted();
        public void TryStartDashAiming();
        public void TryDash(Vector3 cameraForwardDirection);
        public void TryStartHook();
        public void StartPushingTowardsHook();
        public void MoveInputted(Vector2 direction2d);
        public void UnstickFromPlatform();
        public void StickToPlatform(Transform platformTransform);
        public void DisableMoving();
    }
}