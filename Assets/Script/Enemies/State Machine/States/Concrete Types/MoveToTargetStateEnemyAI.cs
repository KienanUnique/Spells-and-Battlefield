using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Interfaces;

namespace Enemies.State_Machine.States.Concrete_Types
{
    public class MoveToTargetStateEnemyAI : StateEnemyAI
    {
        public override ILookPointCalculator LookPointCalculator => new FollowVelocityDirectionLookPointCalculator();
        private IEnemyTarget CurrentTarget => StateMachineControllable.TargetFromTriggersSelector.CurrentTarget;

        protected override void SpecialEnterAction()
        {
            if (CurrentTarget == null)
            {
                return;
            }

            StateMachineControllable.StartFollowingObject(CurrentTarget.MainRigidbody);
        }

        protected override void SpecialExitAction()
        {
            StateMachineControllable.StopFollowingObject();
        }
    }
}