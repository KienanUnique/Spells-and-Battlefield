using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;

namespace Enemies.State_Machine.States
{
    public class IdleStateEnemyAI : StateEnemyAI
    {
        public override ILookPointCalculator LookPointCalculator => new KeepLookDirectionLookPointCalculator();

        protected override void SpecialEnterAction()
        {
            StateMachineControllable.StopMovingToTarget();
        }

        protected override void SpecialExitAction()
        {
        }
    }
}