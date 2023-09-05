namespace Common.Settings.Sections.Movement
{
    public interface IMovementSettingsSection
    {
        float NormalFrictionCoefficient { get; }
        float MoveForce { get; }
        float MaximumSpeed { get; }
    }
}