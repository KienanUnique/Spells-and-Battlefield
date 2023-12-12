using System;
using System.Collections;
using Common.Abstract_Bases.Disableable;
using Common.Event_Invoker_For_Action_Animations;
using Common.Interfaces;
using UnityEngine;

namespace Common.Animator_Status_Controller
{
    public class AnimatorStatusChecker : BaseWithDisabling, IAnimatorStatusChecker
    {
        private const int ActionsAnimationsAnimatorLayer = 1;
        private const float UpdateAnimatorStatusCooldownInSeconds = 0.4f;
        private const string EmptyAnimatorStateName = "Empty State";

        private readonly IEventInvokerForActionAnimations _animationsEventInvoker;
        private readonly Animator _animator;
        private readonly ICoroutineStarter _coroutineStarter;

        private readonly ValueWithReactionOnChange<bool> _isReadyToPlayActionsAnimations =
            new ValueWithReactionOnChange<bool>(true);

        private Coroutine _updateAnimatorStatusCoroutine;

        public AnimatorStatusChecker(IEventInvokerForActionAnimations animationsEventInvoker, Animator animator,
            ICoroutineStarter coroutineStarter)
        {
            _animationsEventInvoker = animationsEventInvoker;
            _animator = animator;
            _coroutineStarter = coroutineStarter;
        }

        public event Action AnimatorReadyToPlayActionsAnimations;
        public event Action ActionAnimationKeyMomentTrigger;

        public bool IsReadyToPlayActionsAnimations => _isReadyToPlayActionsAnimations.Value;

        public void StartChecking()
        {
            if (_updateAnimatorStatusCoroutine == null)
            {
                _updateAnimatorStatusCoroutine =
                    _coroutineStarter.StartCoroutine(UpdateStatusUsingAnimatorContinuously());
            }
        }

        public void StopChecking()
        {
            if (_updateAnimatorStatusCoroutine != null)
            {
                _coroutineStarter.StopCoroutine(_updateAnimatorStatusCoroutine);
                _updateAnimatorStatusCoroutine = null;
            }
        }

        protected override void SubscribeOnEvents()
        {
            _animationsEventInvoker.ActionAnimationStart += OnActionAnimationStart;
            _animationsEventInvoker.ActionAnimationKeyMomentTrigger += OnActionAnimationKeyMomentTrigger;
            _isReadyToPlayActionsAnimations.AfterValueChanged += OnAfterAnimatorStatusChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _animationsEventInvoker.ActionAnimationStart -= OnActionAnimationStart;
            _animationsEventInvoker.ActionAnimationKeyMomentTrigger -= OnActionAnimationKeyMomentTrigger;
            _isReadyToPlayActionsAnimations.AfterValueChanged -= OnAfterAnimatorStatusChanged;
        }

        private void OnAfterAnimatorStatusChanged(bool newStatus)
        {
            if (newStatus)
            {
                AnimatorReadyToPlayActionsAnimations?.Invoke();
            }
        }

        private void OnActionAnimationStart()
        {
            _isReadyToPlayActionsAnimations.Value = false;
        }

        private void OnActionAnimationKeyMomentTrigger()
        {
            ActionAnimationKeyMomentTrigger?.Invoke();
        }

        private IEnumerator UpdateStatusUsingAnimatorContinuously()
        {
            var waitForCooldown = new WaitForSeconds(UpdateAnimatorStatusCooldownInSeconds);
            while (true)
            {
                UpdateStatusUsingAnimator();
                yield return waitForCooldown;
            }
        }

        private void UpdateStatusUsingAnimator()
        {
            _isReadyToPlayActionsAnimations.Value = !_animator.IsInTransition(ActionsAnimationsAnimatorLayer) &&
                                                    _animator.GetCurrentAnimatorStateInfo(
                                                                 ActionsAnimationsAnimatorLayer)
                                                             .IsName(EmptyAnimatorStateName);
        }
    }
}