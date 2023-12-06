using Common.Look;
using Common.Providers;
using UnityEngine;

namespace Enemies.Look.Settings
{
    [CreateAssetMenu(fileName = "Enemy Look Settings Provider",
        menuName = ScriptableObjectsMenuDirectories.EnemiesDirectory + "Enemy Look Settings Provider", order = 0)]
    public class EnemyLookSettingsProvider : ImplementationObjectProviderScriptableObject<ILookSettings>,
        IEnemyLookSettingsProvider
    {
        [SerializeField] private LookSettingsSection _lookSettings;

        public override ILookSettings GetImplementationObject()
        {
            return _lookSettings;
        }
    }
}