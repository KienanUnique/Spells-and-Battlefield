using System;
using Common.Settings.Sections.Movement.Movement_With_Gravity;
using Player.Movement.Hooker.Settings;
using UnityEngine;

namespace Player.Movement.Settings
{
    [Serializable]
    public class PlayerMovementSettingsSection : MovementOnGroundSettingsSection, IPlayerMovementSettings
    {
        [SerializeField] private float _jumpForce = 800f;
        [SerializeField] private float _dashForce = 1500f;
        [SerializeField] private float _wallRunningJumpAngleFromWall = 20f;
        [SerializeField] private float _wallRunningGravityForceMultiplier = 0.08f;
        [SerializeField] private float _dashCooldownSeconds = 5f;
        [SerializeField] private float _dashSpeedLimitationsDisablingForSeconds = 0.3f;
        [SerializeField] private float _coyoteTimeInSeconds = 0.1f;
        [Range(0, 1f)] [SerializeField] private float _flyingFrictionCoefficient = 0.175f;
        [SerializeField] [Min(0)] private float _groundDecreaseAdditionalMaximumSpeedAcceleration = 5f;
        [SerializeField] [Min(0)] private float _airDecreaseAdditionalMaximumSpeedAcceleration = 1f;
        [SerializeField] [Min(0)] private float _wallRunningIncreaseAdditionalMaximumSpeedAcceleration = 4f;
        [SerializeField] [Min(0)] private float _wallRunningIncreaseLimitAdditionalMaximumSpeedAcceleration = 20f;
        [SerializeField] [Min(0)] private float _noInputMovingDecreaseAdditionalMaximumSpeedAcceleration = 99f;
        [SerializeField] private PlayerHookerSettings _hookerSettings;
        [SerializeField] [Min(0)] private float _hookingGravityForceMultiplier = 0.1f;
        [SerializeField] [Min(0)] private float _continuePushingAfterHookSeconds = 0.7f;

        public float GroundDecreaseAdditionalMaximumSpeedAcceleration =>
            _groundDecreaseAdditionalMaximumSpeedAcceleration;

        public float AirDecreaseAdditionalMaximumSpeedAcceleration => _airDecreaseAdditionalMaximumSpeedAcceleration;
        public float FlyingFrictionCoefficient => _flyingFrictionCoefficient;
        public float JumpForce => _jumpForce;
        public float DashForce => _dashForce;
        public float WallRunningJumpAngleTowardsUp => _wallRunningJumpAngleFromWall;
        public float WallRunningGravityForceMultiplier => _wallRunningGravityForceMultiplier;

        public float WallRunningIncreaseAdditionalMaximumSpeedAcceleration =>
            _wallRunningIncreaseAdditionalMaximumSpeedAcceleration;

        public float WallRunningIncreaseLimitAdditionalMaximumSpeedAcceleration =>
            _wallRunningIncreaseLimitAdditionalMaximumSpeedAcceleration;

        public float NoInputMovingDecreaseAdditionalMaximumSpeedAcceleration =>
            _noInputMovingDecreaseAdditionalMaximumSpeedAcceleration;

        public float HookingGravityForceMultiplier => _hookingGravityForceMultiplier;

        public IPlayerHookerSettings HookerSettings => _hookerSettings;

        public float ContinuePushingAfterHookEndSeconds => _continuePushingAfterHookSeconds;

        public float DashCooldownSeconds => _dashCooldownSeconds;
        public float DashSpeedLimitationsDisablingForSeconds => _dashSpeedLimitationsDisablingForSeconds;
        public float CoyoteTimeInSeconds => _coyoteTimeInSeconds;
    }
}