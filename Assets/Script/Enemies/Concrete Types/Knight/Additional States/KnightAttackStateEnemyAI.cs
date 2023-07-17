using System;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Enemies.State_Machine;
using Interfaces;

namespace Enemies.Concrete_Types.Knight.Additional_States
{
    public class KnightAttackStateEnemyAI : StateEnemyAI
    {
        private KnightController _knightController;
        public override ILookPointCalculator LookPointCalculator => new FollowTargetLookPointCalculator();
        private IEnemyTarget CurrentTarget => StateMachineControllable.TargetFromTriggersSelector.CurrentTarget;


        protected override void SpecialEnterAction()
        {
            if (CurrentTarget == null)
            {
                return;
            }

            if (!(StateMachineControllable is KnightController controller))
            {
                throw new InvalidCastException();
            }

            _knightController = controller;

            _knightController.StartSwordAttack(CurrentTarget);
        }

        protected override void SpecialExitAction()
        {
            _knightController.StopSwordAttack();
        }
    }
}