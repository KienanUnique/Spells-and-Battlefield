namespace Enemies.State_Machine.States
{
    public class IdleState : State
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