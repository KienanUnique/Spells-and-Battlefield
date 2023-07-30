using System;
using Enemies.Attack_Target_Selector;
using Enemies.Look_Point_Calculator;
using Enemies.Look_Point_Calculator.Concrete_Types;
using Interfaces;
using UnityEngine;

namespace Enemies.State_Machine.States.Concrete_Types.Melee_Attack
{
    public class MeleeAttackStateEnemyAI : StateEnemyAI
    {
        [SerializeField] private AttackTargetSelectorFromZone _damageTargetSelector;
        [SerializeField] private MeleeAttackStateData _data;
        public override event Action<ILookPointCalculator> NeedChangeLookPointCalculator;
        public override ILookPointCalculator LookPointCalculator => new FollowTargetLookPointCalculator();
        private IEnemyTarget CurrentTarget => StateMachineControllable.TargetFromTriggersSelector.CurrentTarget;

        protected override void SpecialEnterAction()
        {
            if (CurrentTarget == null)
            {
                return;
            }

            SubscribeOnLocalEvents();
            StateMachineControllable.StartPlayingActionAnimation(_data.AnimationData);
            StateMachineControllable.StartFollowingObject(CurrentTarget.MainRigidbody);
        }

        protected override void SpecialExitAction()
        {
            UnsubscribeFromLocalEvents();
            StateMachineControllable.StopFollowingObject();
            StateMachineControllable.StopPlayingActionAnimation();
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            if (IsActivated)
            {
                SubscribeOnLocalEvents();
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            UnsubscribeFromLocalEvents();
        }

        private void SubscribeOnLocalEvents()
        {
            StateMachineControllable.AnimationUseActionMomentTrigger += OnAnimationUseActionMomentTrigger;
        }

        private void UnsubscribeFromLocalEvents()
        {
            StateMachineControllable.AnimationUseActionMomentTrigger -= OnAnimationUseActionMomentTrigger;
        }

        private void OnAnimationUseActionMomentTrigger()
        {
            StateMachineControllable.ApplyEffectsToTargets(_damageTargetSelector.GetTargetsInCollider(),
                _data.HitMechanicEffects);
        }
    }
}