using Enemies.Movement.Provider;
using Enemies.Movement.Setup_Data;
using Settings.Sections.Movement;
using UnityEngine;

namespace Enemies.Movement.Concrete_Types.Movement_Without_Gravity
{
    [CreateAssetMenu(fileName = "Enemy Movement Without Gravity Provider",
        menuName = ScriptableObjectsMenuDirectories.EnemyMovementProvidersDirectory +
                   "Enemy Movement Without Gravity Provider", order = 0)]
    public class EnemyMovementWithoutGravityProvider : EnemyMovementProviderBase
    {
        [SerializeField] private MovementSettingsSection _movementSettings;
        public override IDisableableEnemyMovement GetImplementationObject(IEnemyMovementSetupData setupData)
        {
            return new EnemyMovementWithoutGravity(setupData, _movementSettings, _targetPathfinderSettings);
        }
    }
}