using Common.Settings.Sections.Movement;
using Enemies.Movement.Setup_Data;

namespace Enemies.Movement.Concrete_Types.Movement_Without_Gravity
{
    public class EnemyMovementWithoutGravity : EnemyMovementBase
    {
        public EnemyMovementWithoutGravity(IEnemyMovementSetupData setupData, IMovementSettingsSection movementSettings)
            : base(setupData, movementSettings)
        {
        }
    }
}