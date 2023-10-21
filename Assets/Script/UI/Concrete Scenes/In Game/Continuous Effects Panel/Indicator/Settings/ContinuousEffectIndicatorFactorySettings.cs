using UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Prefab_Provider;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Continuous_Effects_Panel.Indicator.Settings
{
    [CreateAssetMenu(fileName = "Continuous Effect Indicator Factory Settings",
        menuName = ScriptableObjectsMenuDirectories.SystemsSettingsDirectory +
                   "Continuous Effect Indicator Factory Settings", order = 0)]
    public class ContinuousEffectIndicatorFactorySettings : ScriptableObject, IContinuousEffectIndicatorFactorySettings
    {
        [Min(1)] [SerializeField] private int _objectPooledIndicatorsCount = 5;
        [SerializeField] private ContinuousEffectIndicatorPrefabProvider _prefabProvider;

        public int ObjectPooledIndicatorsCount => _objectPooledIndicatorsCount;
        public IContinuousEffectIndicatorPrefabProvider PrefabProvider => _prefabProvider;
    }
}