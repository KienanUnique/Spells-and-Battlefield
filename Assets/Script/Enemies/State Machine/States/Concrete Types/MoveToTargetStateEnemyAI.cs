using System;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Enemies.Movement.Enemy_Data_For_Moving;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types
{
    public class MoveToTargetStateEnemyAI : StateEnemyAI
    {
        [SerializeField] private EnemyDataForMoving _dataForMoving;
        public override event Action<ILookPointCalculator> NeedChangeLookPointCalculator;
        public override ILookPointCalculator LookPointCalculator => new FollowVelocityDirectionLookPointCalculator();

        protected override void SpecialEnterAction()
        {
            StateMachineControllable.StartKeepingCurrentTargetOnDistance(_dataForMoving);
        }

        protected override void SpecialExitAction()
        {
            StateMachineControllable.StopMoving();
        }
    }
}