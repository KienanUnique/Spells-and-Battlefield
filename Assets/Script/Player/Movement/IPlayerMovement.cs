using System;
using Common.Abstract_Bases.Movement;
using Common.Readonly_Rigidbody;
using Interfaces;
using UnityEngine;

namespace Player.Movement
{
    public interface IPlayerMovement : IMovementBase, IPlayerDashInformation
    {
        public event Action Land;
        public event Action GroundJump;
        public event Action AirJump;
        public event Action Fall;
        public event Action<WallDirection> StartWallRunning;
        public event Action<WallDirection> WallRunningDirectionChanged;
        public event Action EndWallRunning;
        public Vector2 NormalizedVelocityDirectionXY { get; }
        public float RatioOfCurrentVelocityToMaximumVelocity { get; }
        public Vector3 CurrentPosition { get; }
        public IReadonlyRigidbody MainRigidbody { get; }
        public void TryJumpInputted();
        public void TryStartDashAiming();
        public void TryDash(Vector3 cameraForwardDirection);
        public void MoveInputted(Vector2 direction2d);
        public void AddForce(Vector3 force, ForceMode mode);
        public void UnstickFromPlatform();
        public void StickToPlatform(Transform platformTransform);
    }
}