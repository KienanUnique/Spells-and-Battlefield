namespace Enemies.State_Machine.States
{
    public class MoveToTargetStateEnemyAI : StateEnemyAI
    {
        protected override void SpecialEnterAction()
        {
            StateMachineControllable.StartMovingToTarget(StateMachineControllable.Target.MainTransform);
        }

        protected override void SpecialExitAction()
        {
            StateMachineControllable.StopMovingToTarget();
        }
    }
}