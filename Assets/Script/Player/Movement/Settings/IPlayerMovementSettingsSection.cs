using Common.Settings.Sections.Movement.Movement_With_Gravity;

namespace Player.Movement.Settings
{
    public interface IPlayerMovementSettings : IMovementOnGroundSettingsSection
    {
        float FlyingFrictionCoefficient { get; }
        float JumpForce { get; }
        float DashForce { get; }
        float WallRunningGravityForce { get; }
        float DashCooldownSeconds { get; }
        float DashSpeedLimitationsDisablingForSeconds { get; }
    }
}