using Common.Abstract_Bases.Visual;
using Common.Animation_Data;
using Enemies.Visual.Dissolve_Effect_Controller;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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
        private readonly IDissolveEffectController _dissolveEffectController;

        public EnemyVisual(RigBuilder rigBuilder, Animator characterAnimator,
            AnimatorOverrideController baseAnimatorOverrideController, AnimationClip emptyActionAnimationClip,
            IDissolveEffectController dissolveEffectController) : base(rigBuilder, characterAnimator)
        {
            _baseAnimatorOverrideController = baseAnimatorOverrideController;
            _emptyActionAnimationClip = emptyActionAnimationClip;
            _dissolveEffectController = dissolveEffectController;
            ApplyRuntimeAnimatorController(_baseAnimatorOverrideController);
        }

        public void PlayActionAnimation(IAnimationData animationData)
        {
            ApplyAnimationOverride(_baseAnimatorOverrideController, _emptyActionAnimationClip, animationData.Clip);

            _characterAnimator.SetFloat(ActionFloatAnimationSpeedHash, animationData.AnimationSpeed);
            _characterAnimator.SetTrigger(ActionTriggerHash);
        }

        public void UpdateMovingData(bool isRunning)
        {
            _characterAnimator.SetBool(IsRunningBoolHash, isRunning);
        }

        public void PlayDieAnimation()
        {
            _rigBuilder.enabled = false;
            _characterAnimator.SetTrigger(DieTriggerHash);
            _dissolveEffectController.Disappear();
        }

        private void ApplyRuntimeAnimatorController(RuntimeAnimatorController animatorOverrideController)
        {
            _characterAnimator.runtimeAnimatorController = animatorOverrideController;
        }
    }
}