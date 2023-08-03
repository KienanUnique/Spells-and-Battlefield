using Common.Abstract_Bases.Visual;
using Common.Animation_Data;
using UnityEngine;

namespace Enemies.Visual
{
    public class EnemyVisual : VisualBase, IEnemyVisual
    {
        private static readonly int ActionTriggerHash = Animator.StringToHash("Do Action");
        private static readonly int ActionFloatAnimationSpeedHash = Animator.StringToHash("Use Action Speed");
        private static readonly int IsRunningBoolHash = Animator.StringToHash("Is Running");
        private static readonly int DieTriggerHash = Animator.StringToHash("Die");
        private readonly AnimatorOverrideController _baseAnimatorOverrideController;
        private readonly AnimationClip _emptyActionAnimationClip;

        public EnemyVisual(Animator characterAnimator, AnimatorOverrideController baseAnimatorOverrideController,
            AnimationClip emptyActionAnimationClip) : base(characterAnimator)
        {
            _baseAnimatorOverrideController = baseAnimatorOverrideController;
            _emptyActionAnimationClip = emptyActionAnimationClip;
            ApplyRuntimeAnimatorController(_baseAnimatorOverrideController);
        }

        public void UpdateMovingData(bool isRunning)
        {
            _characterAnimator.SetBool(IsRunningBoolHash, isRunning);
        }

        public void PlayDieAnimation()
        {
            _characterAnimator.SetTrigger(DieTriggerHash);
        }

        public void PlayActionAnimation(IAnimationData animationData)
        {
            ApplyAnimationOverride(_baseAnimatorOverrideController, _emptyActionAnimationClip, animationData.Clip);

            _characterAnimator.SetFloat(ActionFloatAnimationSpeedHash, animationData.AnimationSpeed);
            _characterAnimator.SetTrigger(ActionTriggerHash);
        }

        private void ApplyRuntimeAnimatorController(RuntimeAnimatorController animatorOverrideController)
        {
            _characterAnimator.runtimeAnimatorController = animatorOverrideController;
        }
    }
}