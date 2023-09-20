using Common.Settings.Sections.Movement.Movement_With_Gravity;

namespace Player.Movement.Settings
{
    public interface IPlayerMovementSettings : IMovementOnGroundSettingsSection
    {
        public float FlyingFrictionCoefficient { get; }
        public float JumpForce { get; }
        public float DashForce { get; }
        public float WallRunningGravityForceMultiplier { get; }
        public float DashCooldownSeconds { get; }
        public float DashSpeedLimitationsDisablingForSeconds { get; }
        public float CoyoteTimeInSeconds { get; }
    }
}