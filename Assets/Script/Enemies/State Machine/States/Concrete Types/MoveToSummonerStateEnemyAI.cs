using System;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Enemies.Movement.Enemy_Data_For_Moving;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types
{
    public class MoveToSummonerStateEnemyAI : StateEnemyAI
    {
        [SerializeField] private EnemyDataForMoving _dataForMoving;
        public override ILookPointCalculator LookPointCalculator => new FollowVelocityDirectionLookPointCalculator();

        protected override void SpecialReactionOnStateStatusChange(StateEnemyAIStatus newStatus)
        {
            switch (newStatus)
            {
                case StateEnemyAIStatus.NonActive:
                    StateMachineControllable.StopMoving();
                    break;
                case StateEnemyAIStatus.Active:
                    StateMachineControllable.StartKeepingSummonerOnDistance(_dataForMoving);
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