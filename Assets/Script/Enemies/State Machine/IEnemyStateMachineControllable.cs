using Common.Readonly_Transform;
using Enemies.Target_Selector_From_Triggers;

namespace Enemies.State_Machine
{
    public interface IEnemyStateMachineControllable
    {
        public IEnemyTargetFromTriggersSelector TargetFromTriggersSelector { get; }
        public void StartMovingToTarget(IReadonlyTransform target);
        public void StopMovingToTarget();
    }
}