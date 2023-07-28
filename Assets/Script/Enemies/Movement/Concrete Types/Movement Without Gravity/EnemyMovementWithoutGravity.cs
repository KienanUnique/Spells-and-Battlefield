using Enemies.Movement.Setup_Data;
using General_Settings_in_Scriptable_Objects.Sections;
using Settings.Sections.Movement;

namespace Enemies.Movement.Concrete_Types.Movement_Without_Gravity
{
    public class EnemyMovementWithoutGravity : EnemyMovementBase
    {
        public EnemyMovementWithoutGravity(IEnemyMovementSetupData setupData, MovementSettingsSection movementSettings,
            TargetPathfinderSettingsSection targetPathfinderSettings) : base(setupData, movementSettings,
            targetPathfinderSettings)
        {
        }
    }
}