namespace Common.Settings.Sections.Movement.Movement_With_Gravity
{
    public interface IMovementOnGroundSettingsSection : IMovementSettingsSection
    {
        public float NormalGravityForce { get; }
        public float NormalGravityForceMultiplier { get; }
    }
}