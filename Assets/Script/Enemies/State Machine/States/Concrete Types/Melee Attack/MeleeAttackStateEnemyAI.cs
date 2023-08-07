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

        protected override void SpecialEnterAction()
        {
            SubscribeOnLocalEvents();
            StateMachineControllable.StartKeepingCurrentTargetOnDistance(_data.DataForMoving);
            Attack();
        }

        protected override void SpecialExitAction()
        {
            UnsubscribeFromLocalEvents();
            StateMachineControllable.StopMoving();
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
            Attack();
        }

        private void OnActionAnimationKeyMomentTrigger()
        {
            StateMachineControllable.ApplyEffectsToTargets(_damageTargetSelector.GetTargetsInCollider(),
                _data.HitMechanicEffects);
        }

        private void Attack()
        {
            StateMachineControllable.PlayActionAnimation(_data.AnimationData);
        }
    }
}