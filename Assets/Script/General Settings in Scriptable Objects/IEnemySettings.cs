namespace General_Settings_in_Scriptable_Objects
{
    public interface IEnemySettings
    {
        public CharacterSettingsSection CharacterSettings { get; }
        public MovementSettingsSectionBase MovementSettings { get; }
        public TargetPathfinderSettingsSection TargetPathfinderSettingsSection { get; }
    }
}