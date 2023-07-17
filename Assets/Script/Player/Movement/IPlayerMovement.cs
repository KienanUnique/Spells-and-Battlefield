using System;
using Common;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using UnityEngine;

namespace Player.Movement
{
    public interface IPlayerMovement
    {
        public event Action Land;
        public event Action GroundJump;
        public event Action AirJump;
        public event Action Fall;
        public event Action<WallDirection> StartWallRunning;
        public event Action<WallDirection> WallRunningDirectionChanged;
        public event Action EndWallRunning;
        public event Action<float> DashCooldownTimerTick;
        public event Action DashCooldownFinished;
        public event Action DashAiming;
        public event Action Dashed;
        public Vector2 NormalizedVelocityDirectionXY { get; }
        public float RatioOfCurrentVelocityToMaximumVelocity { get; }
        public Vector3 CurrentPosition { get; }
        public IReadonlyRigidbody MainRigidbody { get; }
        public void TryJumpInputted();
        public void TryStartDashAiming();
        public void TryDash(Vector3 cameraForwardDirection);
        public void MoveInputted(Vector2 direction2d);
        public void AddForce(Vector3 force, ForceMode mode);
        public void MultiplySpeedRatioBy(float speedRatio);
        public void DivideSpeedRatioBy(float speedRatio);
        public void UnstickFromPlatform();
        public void StickToPlatform(Transform platformTransform);
    }
}