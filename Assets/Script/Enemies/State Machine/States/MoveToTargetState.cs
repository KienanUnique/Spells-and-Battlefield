namespace Enemies.State_Machine.States
{
    public class MoveToTargetState : State
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