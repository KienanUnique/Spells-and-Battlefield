namespace Common.Settings.Sections.Movement
{
    public interface IMovementSettingsSection
    {
        public float NormalFrictionCoefficient { get; }
        public float MoveForce { get; }
        public float MaximumSpeed { get; }
        public float HookForce { get; }
    }
}