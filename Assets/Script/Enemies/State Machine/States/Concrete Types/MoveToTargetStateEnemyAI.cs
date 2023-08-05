using System;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Enemies.Movement.Enemy_Data_For_Moving;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types
{
    public class MoveToTargetStateEnemyAI : StateEnemyAI
    {
        [SerializeField] private EnemyDataForMoving _dataForMoving;
        public override event Action<ILookPointCalculator> NeedChangeLookPointCalculator;
        public override ILookPointCalculator LookPointCalculator => new FollowVelocityDirectionLookPointCalculator();
        private IEnemyTarget CurrentTarget => StateMachineControllable.TargetFromTriggersSelector.CurrentTarget;

        protected override void SpecialEnterAction()
        {
            if (CurrentTarget == null)
            {
                return;
            }

            StateMachineControllable.StartKeepingTransformOnDistance(CurrentTarget.MainRigidbody, _dataForMoving);
        }

        protected override void SpecialExitAction()
        {
            StateMachineControllable.StopMoving();
        }
    }
}