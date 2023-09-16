using System;
using UnityEngine;

namespace Enemies.State_Machine.Transition_Conditions.Concrete_Types
{
    public class CheckMethodOfSpawnTransitionConditionEnemyAI : TransitionConditionEnemyAIBase
    {
        [SerializeField] private Creators _isCreatedBy;

        private enum Creators
        {
            Spawner, Summoner
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

        public override bool IsConditionCompleted
        {
            get
            {
                return _isCreatedBy switch
                {
                    Creators.Spawner => StateMachineControllable.Summoner == null,
                    Creators.Summoner => StateMachineControllable.Summoner != null,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        protected override void HandleStartCheckingConditions()
        {
        }

        protected override void HandleStopCheckingConditions()
        {
        }
    }
}