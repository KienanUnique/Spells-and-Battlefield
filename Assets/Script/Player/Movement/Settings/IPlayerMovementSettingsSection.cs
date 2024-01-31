using Common.Settings.Sections.Movement.Movement_With_Gravity;
using Player.Movement.Hooker.Settings;

namespace Player.Movement.Settings
{
    public interface IPlayerMovementSettings : IMovementOnGroundSettingsSection
    {
        public float FlyingFrictionCoefficient { get; }
        public float JumpForce { get; }
        public float WallRunningJumpAngleTowardsUp { get; }
        public float DashForce { get; }
        public float WallRunningGravityForceMultiplier { get; }
        public float DashCooldownSeconds { get; }
        public float DashSpeedLimitationsDisablingForSeconds { get; }
        public float CoyoteTimeInSeconds { get; }
        public float GroundDecreaseAdditionalMaximumSpeedAcceleration { get; }
        public float AirDecreaseAdditionalMaximumSpeedAcceleration { get; }
        public float WallRunningIncreaseAdditionalMaximumSpeedAcceleration { get; }
        public float WallRunningIncreaseLimitAdditionalMaximumSpeedAcceleration { get; }
        public float NoInputMovingDecreaseAdditionalMaximumSpeedAcceleration { get; }
        public float HookingGravityForceMultiplier { get; }
        public float DisableSpeedLimitationAfterHookSeconds { get; }
        public IPlayerHookerSettings HookerSettings { get; }
    }
}