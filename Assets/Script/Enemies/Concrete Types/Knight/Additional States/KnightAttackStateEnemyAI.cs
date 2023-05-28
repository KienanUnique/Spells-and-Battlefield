using System;
using Enemies.State_Machine;
using Interfaces;

namespace Enemies.Concrete_Types.Knight.Additional_States
{
    public class KnightAttackStateEnemyAI : StateEnemyAI
    {
        private KnightController _knightController;
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

            _knightController.StartSwordAttack(CurrentTarget.MainTransform);
        }

        protected override void SpecialExitAction()
        {
            _knightController.StopSwordAttack();
        }
    }
}