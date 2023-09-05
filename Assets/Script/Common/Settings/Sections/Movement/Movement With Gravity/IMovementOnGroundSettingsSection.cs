namespace Common.Settings.Sections.Movement.Movement_With_Gravity
{
    public interface IMovementOnGroundSettingsSection : IMovementSettingsSection
    {
        float NormalGravityForce { get; }
    }
}