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
        protected readonly ValueWithReactionOnChange<AnimatorStatus> _currentAnimatorStatus =
            new ValueWithReactionOnChange<AnimatorStatus>(AnimatorStatus.Idle);
        
        private const float UpdateAnimatorStatusCooldownInSeconds = 0.4f;
        private const string EmptyAnimatorStateName = "Empty State";
        private const string ActionsAnimationsAnimatorLayerName = "Upper Body";

        private readonly int _actionsAnimationsAnimatorLayer;
        private readonly IEventInvokerForActionAnimations _animationsEventInvoker;
        private readonly Animator _animator;
        private readonly ICoroutineStarter _coroutineStarter;

        private Coroutine _updateAnimatorStatusCoroutine;

        public AnimatorStatusChecker(IEventInvokerForActionAnimations animationsEventInvoker, Animator animator,
            ICoroutineStarter coroutineStarter)
        {
            _animationsEventInvoker = animationsEventInvoker;
            _animator = animator;
            _coroutineStarter = coroutineStarter;
            _actionsAnimationsAnimatorLayer = _animator.GetLayerIndex(ActionsAnimationsAnimatorLayerName);
        }

        public event Action AnimatorReadyToPlayActionsAnimations;
        public event Action ActionAnimationKeyMomentTrigger;

        public bool IsReadyToPlayActionAnimations => _currentAnimatorStatus.Value == AnimatorStatus.Idle;

        public void StartChecking()
        {
            _updateAnimatorStatusCoroutine ??=
                _coroutineStarter.StartCoroutine(UpdateStatusUsingAnimatorContinuously());
        }

        public void StopChecking()
        {
            if (_updateAnimatorStatusCoroutine == null)
            {
                return;
            }

            _coroutineStarter.StopCoroutine(_updateAnimatorStatusCoroutine);
            _updateAnimatorStatusCoroutine = null;
        }

        public void HandleActionAnimationPlay()
        {
            OnActionAnimationStart();
        }

        public void HandleActionAnimationCancel()
        {
            _currentAnimatorStatus.Value = AnimatorStatus.WaitingActionAnimationEnd;
        }

        protected override void SubscribeOnEvents()
        {
            _animationsEventInvoker.ActionAnimationStart += OnActionAnimationStart;
            _animationsEventInvoker.ActionAnimationKeyMomentTrigger += OnActionAnimationKeyMomentTrigger;
            _currentAnimatorStatus.AfterValueChanged += OnAfterAnimatorStatusChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _animationsEventInvoker.ActionAnimationStart -= OnActionAnimationStart;
            _animationsEventInvoker.ActionAnimationKeyMomentTrigger -= OnActionAnimationKeyMomentTrigger;
            _currentAnimatorStatus.AfterValueChanged -= OnAfterAnimatorStatusChanged;
        }

        private void OnAfterAnimatorStatusChanged(AnimatorStatus newStatus)
        {
            if (newStatus == AnimatorStatus.Idle)
            {
                AnimatorReadyToPlayActionsAnimations?.Invoke();
            }
        }

        private void OnActionAnimationStart()
        {
            _currentAnimatorStatus.Value = AnimatorStatus.WaitingActionAnimationStart;
        }

        private void OnActionAnimationKeyMomentTrigger()
        {
            _currentAnimatorStatus.Value = AnimatorStatus.WaitingActionAnimationEnd;
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
            bool isInEmptyState = !_animator.IsInTransition(_actionsAnimationsAnimatorLayer) &&
                                  _animator.GetCurrentAnimatorStateInfo(_actionsAnimationsAnimatorLayer)
                                           .IsName(EmptyAnimatorStateName);
            _currentAnimatorStatus.Value = isInEmptyState switch
            {
                false when _currentAnimatorStatus.Value == AnimatorStatus.WaitingActionAnimationStart =>
                    AnimatorStatus.WaitingActionAnimationEnd,
                true when _currentAnimatorStatus.Value == AnimatorStatus.WaitingActionAnimationEnd =>
                    AnimatorStatus.Idle,
                _ => _currentAnimatorStatus.Value
            };
        }
    }
}