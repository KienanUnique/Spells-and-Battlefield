namespace Enemies.State_Machine.States
{
    public class IdleStateEnemyAI : StateEnemyAI
    {
        protected override void SpecialEnterAction()
        {
            StateMachineControllable.StopMovingToTarget();
        }

        protected override void SpecialExitAction()
        {
        }
    }
}