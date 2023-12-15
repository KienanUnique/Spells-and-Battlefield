using System;

namespace Common.Animator_Status_Controller
{
    public interface IReadonlyAnimatorStatusChecker
    {
        public event Action AnimatorReadyToPlayActionsAnimations;
        public event Action ActionAnimationKeyMomentTrigger;

        public bool IsReadyToPlayActionAnimations { get; }
    }
}