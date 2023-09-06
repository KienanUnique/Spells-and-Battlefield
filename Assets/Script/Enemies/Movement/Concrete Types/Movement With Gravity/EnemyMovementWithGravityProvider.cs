using Common.Settings.Sections.Movement.Movement_With_Gravity;
using Enemies.Movement.Provider;
using Enemies.Movement.Setup_Data;
using UnityEngine;

namespace Enemies.Movement.Concrete_Types.Movement_With_Gravity
{
    [CreateAssetMenu(fileName = "Enemy Movement With Gravity Provider",
        menuName = ScriptableObjectsMenuDirectories.EnemyMovementProvidersDirectory +
                   "Enemy Movement With Gravity Provider", order = 0)]
    public class EnemyMovementWithGravityProvider : EnemyMovementProviderBase
    {
        [SerializeField] private MovementOnGroundSettingsSection _movementSettings;

        public override IDisableableEnemyMovement GetImplementationObject(IEnemyMovementSetupData setupData)
        {
            return new EnemyMovementWithGravity(setupData, _movementSettings);
        }
    }
}