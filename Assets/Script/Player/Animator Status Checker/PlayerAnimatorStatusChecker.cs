using System;
using Common.Animator_Status_Controller;
using Common.Event_Invoker_For_Action_Animations;
using Common.Interfaces;
using UnityEngine;

namespace Player.Animator_Status_Checker
{
    public class PlayerAnimatorStatusChecker : AnimatorStatusChecker, IPlayerAnimatorStatusChecker
    {
        private readonly IPlayerEventInvokerForActionAnimations _playerEventInvoker;

        public PlayerAnimatorStatusChecker(IEventInvokerForActionAnimations animationsEventInvoker, Animator animator,
            ICoroutineStarter coroutineStarter, IPlayerEventInvokerForActionAnimations playerEventInvoker) : base(
            animationsEventInvoker, animator, coroutineStarter)
        {
            _playerEventInvoker = playerEventInvoker;
        }

        public event Action HookKeyMomentTrigger;

        public void HandleHookStart()
        {
            _currentAnimatorStatus.Value = AnimatorStatus.WaitingActionAnimationStart;
        }

        public void HandleHookEnd()
        {
            _currentAnimatorStatus.Value = AnimatorStatus.WaitingActionAnimationEnd;
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _playerEventInvoker.HookKeyMomentTrigger += OnHookKeyMomentTrigger;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            _playerEventInvoker.HookKeyMomentTrigger -= OnHookKeyMomentTrigger;
        }

        private void OnHookKeyMomentTrigger()
        {
            HookKeyMomentTrigger?.Invoke();
        }
    }
}