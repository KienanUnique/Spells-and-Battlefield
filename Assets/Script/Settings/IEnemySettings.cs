using General_Settings_in_Scriptable_Objects.Sections;

namespace General_Settings_in_Scriptable_Objects
{
    public interface IEnemySettings
    {
        public CharacterSettingsSection CharacterSettings { get; }
        public MovementSettingsSection MovementSettings { get; }
        public TargetPathfinderSettingsSection TargetPathfinderSettingsSection { get; }
    }
}