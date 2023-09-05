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
        [SerializeField] private float _wallRunningGravityForce = 2;
        [SerializeField] private float _dashCooldownSeconds = 5f;
        [SerializeField] private float _dashSpeedLimitationsDisablingForSeconds = 0.3f;
        [Range(0, 1f)] [SerializeField] private float _flyingFrictionCoefficient = 0.175f;

        public float FlyingFrictionCoefficient => _flyingFrictionCoefficient;
        public float JumpForce => _jumpForce;
        public float DashForce => _dashForce;
        public float WallRunningGravityForce => _wallRunningGravityForce;
        public float DashCooldownSeconds => _dashCooldownSeconds;
        public float DashSpeedLimitationsDisablingForSeconds => _dashSpeedLimitationsDisablingForSeconds;
    }
}