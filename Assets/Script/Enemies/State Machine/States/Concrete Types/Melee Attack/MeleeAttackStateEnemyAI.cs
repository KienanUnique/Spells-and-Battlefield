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
        public override ILookPointCalculator LookPointCalculator => new FollowTargetLookPointCalculator();
        private bool IsReadyToPlayActionsAnimations => AnimatorStatusChecker.IsReadyToPlayActionAnimations;

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
                    SubscribeOnLocalEvents();
                    StateMachineControllable.StartKeepingCurrentTargetOnDistance(_data.DataForMoving);
                    Attack();
                    break;
                case StateEnemyAIStatus.Exiting:
                    if (IsReadyToPlayActionsAnimations)
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
            AnimatorStatusChecker.AnimatorReadyToPlayActionsAnimations += OnAnimatorReadyToPlayActionsAnimations;
            AnimatorStatusChecker.ActionAnimationKeyMomentTrigger += OnActionAnimationKeyMomentTrigger;
        }

        private void UnsubscribeFromLocalEvents()
        {
            AnimatorStatusChecker.AnimatorReadyToPlayActionsAnimations -= OnAnimatorReadyToPlayActionsAnimations;
            AnimatorStatusChecker.ActionAnimationKeyMomentTrigger -= OnActionAnimationKeyMomentTrigger;
        }

        private void OnAnimatorReadyToPlayActionsAnimations()
        {
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
            if (CurrentStatus == StateEnemyAIStatus.Active)
            {
                StateMachineControllable.ApplyEffectsToTargets(_damageTargetSelector.GetTargetsInCollider(),
                    _data.HitMechanicEffects);
            }
        }

        private void Attack()
        {
            if (CurrentStatus == StateEnemyAIStatus.Active)
            {
                StateMachineControllable.PlayActionAnimation(_data.AnimationData);
            }
        }
    }
}