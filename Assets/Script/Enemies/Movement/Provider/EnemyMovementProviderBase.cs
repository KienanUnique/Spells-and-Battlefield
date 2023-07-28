using Enemies.Movement.Setup_Data;
using General_Settings_in_Scriptable_Objects.Sections;
using UnityEngine;

namespace Enemies.Movement.Provider
{
    [CreateAssetMenu(fileName = "Enemy Movement Without Gravity Provider",
        menuName = ScriptableObjectsMenuDirectories.EnemyMovementProvidersDirectory +
                   "Enemy Movement Without Gravity Provider", order = 0)]
    public abstract class EnemyMovementProviderBase : ScriptableObject, IEnemyMovementProviderBase
    {
        [SerializeField] protected TargetPathfinderSettingsSection _targetPathfinderSettings;
        public abstract IDisableableEnemyMovement GetImplementationObject(IEnemyMovementSetupData setupData);
    }
}