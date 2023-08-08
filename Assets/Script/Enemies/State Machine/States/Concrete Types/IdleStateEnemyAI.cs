using System;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;

namespace Enemies.State_Machine.States.Concrete_Types
{
    public class IdleStateEnemyAI : StateEnemyAI
    {
        public override ILookPointCalculator LookPointCalculator => new KeepLookDirectionLookPointCalculator();

        protected override void SpecialReactionOnStateStatusChange(StateEnemyAIStatus newStatus)
        {
            switch (newStatus)
            {
                case StateEnemyAIStatus.NonActive:
                    break;
                case StateEnemyAIStatus.Active:
                    break;
                case StateEnemyAIStatus.Exiting:
                    HandleExitFromState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }
    }
}