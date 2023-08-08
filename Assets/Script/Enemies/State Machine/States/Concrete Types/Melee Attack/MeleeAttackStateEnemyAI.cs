using System;
using Enemies.Attack_Target_Selector;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Melee_Attack
{
    public class MeleeAttackStateEnemyAI : StateEnemyAI
    {
        [SerializeField] private AttackTargetSelectorFromZone _damageTargetSelector;
        [SerializeField] private MeleeAttackStateData _data;
        private bool _isWaitingForAnimationFinish;
        public override ILookPointCalculator LookPointCalculator => new FollowTargetLookPointCalculator();

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            if (CurrentStatus == StateEnemyAIStatus.Active)
            {
                SubscribeOnLocalEvents();
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            UnsubscribeFromLocalEvents();
        }

        protected override void SpecialReactionOnStateStatusChange(StateEnemyAIStatus newStatus)
        {
            switch (newStatus)
            {
                case StateEnemyAIStatus.NonActive:
                    UnsubscribeFromLocalEvents();
                    StateMachineControllable.StopMoving();
                    break;
                case StateEnemyAIStatus.Active:
                    _isWaitingForAnimationFinish = false;
                    SubscribeOnLocalEvents();
                    StateMachineControllable.StartKeepingCurrentTargetOnDistance(_data.DataForMoving);
                    Attack();
                    break;
                case StateEnemyAIStatus.Exiting:
                    if (!_isWaitingForAnimationFinish)
                    {
                        HandleExitFromState();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }

        private void SubscribeOnLocalEvents()
        {
            StateMachineControllable.ActionAnimationEnd += OnActionAnimationEnd;
            StateMachineControllable.ActionAnimationKeyMomentTrigger += OnActionAnimationKeyMomentTrigger;
        }

        private void UnsubscribeFromLocalEvents()
        {
            StateMachineControllable.ActionAnimationEnd -= OnActionAnimationEnd;
            StateMachineControllable.ActionAnimationKeyMomentTrigger -= OnActionAnimationKeyMomentTrigger;
        }

        private void OnActionAnimationEnd()
        {
            _isWaitingForAnimationFinish = false;
            switch (CurrentStatus)
            {
                case StateEnemyAIStatus.Exiting:
                    HandleExitFromState();
                    break;
                case StateEnemyAIStatus.Active:
                    HandleCompletedAction();
                    Attack();
                    break;
            }
        }

        private void OnActionAnimationKeyMomentTrigger()
        {
            StateMachineControllable.ApplyEffectsToTargets(_damageTargetSelector.GetTargetsInCollider(),
                _data.HitMechanicEffects);
        }

        private void Attack()
        {
            if (CurrentStatus == StateEnemyAIStatus.Active)
            {
                _isWaitingForAnimationFinish = true;
                StateMachineControllable.PlayActionAnimation(_data.AnimationData);
            }
        }
    }
}