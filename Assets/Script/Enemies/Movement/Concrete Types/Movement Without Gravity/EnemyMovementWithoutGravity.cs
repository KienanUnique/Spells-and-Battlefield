using Enemies.Movement.Setup_Data;
using Settings.Sections;
using Settings.Sections.Movement;

namespace Enemies.Movement.Concrete_Types.Movement_Without_Gravity
{
    public class EnemyMovementWithoutGravity : EnemyMovementBase
    {
        public EnemyMovementWithoutGravity(IEnemyMovementSetupData setupData, MovementSettingsSection movementSettings)
            : base(setupData, movementSettings)
        {
        }
    }
}