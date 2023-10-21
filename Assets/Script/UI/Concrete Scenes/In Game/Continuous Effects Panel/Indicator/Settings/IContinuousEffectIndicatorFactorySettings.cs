using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Prefab_Provider;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Settings
{
    public interface IContinuousEffectIndicatorFactorySettings
    {
        public int ObjectPooledIndicatorsCount { get; }
        public IContinuousEffectIndicatorPrefabProvider PrefabProvider { get; }
    }
}