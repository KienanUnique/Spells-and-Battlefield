using System;
using Common.Settings.Sections.Movement.Movement_With_Gravity;
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

        public float FlyingFrictionCoefficient => _flyingFrictionCoefficient;
        public float JumpForce => _jumpForce;
        public float WallRunningJumpAngleTowardsUp => _wallRunningJumpAngleFromWall;
        public float DashForce => _dashForce;
        public float WallRunningGravityForceMultiplier => _wallRunningGravityForceMultiplier;
        public float DashCooldownSeconds => _dashCooldownSeconds;
        public float DashSpeedLimitationsDisablingForSeconds => _dashSpeedLimitationsDisablingForSeconds;
        public float CoyoteTimeInSeconds => _coyoteTimeInSeconds;
    }
}