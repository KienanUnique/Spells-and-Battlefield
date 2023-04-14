using System;
using Enemies.State_Machine;

namespace Enemies.Knight.Additional_States
{
    public class KnightAttackStateEnemyAI : StateEnemyAI
    {
        private KnightController _knightController;

        protected override void SpecialEnterAction()
        {
            if (!(StateMachineControllable is KnightController controller))
            {
                throw new InvalidCastException();
            }

            _knightController = controller;

            _knightController.StartSwordAttack(StateMachineControllable.Target.MainTransform);
        }

        protected override void SpecialExitAction()
        {
            _knightController.StopSwordAttack();
        }
    }
}