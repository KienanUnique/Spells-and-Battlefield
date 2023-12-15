using Common.Abstract_Bases.Visual.Settings;

namespace Enemies.General_Settings
{
    public interface IGeneralEnemySettings
    {
        public IVisualSettings VisualSettings { get; }
        public float DelayInSecondsBeforeDestroy { get; }
        public float TargetSelectorUpdateCooldownInSeconds { get; }
    }
}