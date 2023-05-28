using Interfaces;

namespace Enemies.State_Machine.States
{
    public class MoveToTargetStateEnemyAI : StateEnemyAI
    {
        private IEnemyTarget CurrentTarget => StateMachineControllable.TargetSelector.CurrentTarget;
        protected override void SpecialEnterAction()
        {
            if (CurrentTarget == null)
            {
                return;
            }
            StateMachineControllable.StartMovingToTarget(CurrentTarget.MainTransform);
        }

        protected override void SpecialExitAction()
        {
            StateMachineControllable.StopMovingToTarget();
        }
    }
}