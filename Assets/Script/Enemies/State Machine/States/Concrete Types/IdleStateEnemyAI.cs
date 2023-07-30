using System;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;

namespace Enemies.State_Machine.States.Concrete_Types
{
    public class IdleStateEnemyAI : StateEnemyAI
    {
        public override event Action<ILookPointCalculator> NeedChangeLookPointCalculator;
        public override ILookPointCalculator LookPointCalculator => new KeepLookDirectionLookPointCalculator();

        protected override void SpecialEnterAction()
        {
            StateMachineControllable.StopFollowingObject();
        }

        protected override void SpecialExitAction()
        {
        }
    }
}