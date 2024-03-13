using System.Collections.Generic;
using Common.Abstract_Bases.Visual.Settings;
using Common.Animation_Data;
using Common.Animation_Data.Continuous_Action;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Common.Abstract_Bases.Visual
{
    public abstract class VisualBase
    {
        private static readonly int CancelActionAnimationTriggerHash = Animator.StringToHash("Cancel Action");
        private static readonly int PlayContinuousActionTriggerHash = Animator.StringToHash("Play Continuous Action");

        private static readonly int PrepareContinuousActionFloatSpeedHash =
            Animator.StringToHash("Prepare Continuous Action Speed");

        private static readonly int ContinuousActionFloatSpeedHash = Animator.StringToHash("Continuous Action Speed");
        private static readonly int PlayActionTriggerHash = Animator.StringToHash("Play Action");
        private static readonly int ActionFloatSpeedHash = Animator.StringToHash("Action Speed");

        protected readonly Animator _characterAnimator;
        protected readonly RigBuilder _rigBuilder;

        protected VisualBase(RigBuilder rigBuilder, Animator characterAnimator)
        {
            _rigBuilder = rigBuilder;
            _characterAnimator = characterAnimator;
        }

        protected abstract IVisualSettings Settings { get; }
        protected abstract AnimatorOverrideController OverrideController { get; }

        public void PlayActionAnimation(IAnimationData animationData)
        {
            _characterAnimator.ResetTrigger(CancelActionAnimationTriggerHash);
            ApplyAnimationOverride(OverrideController,
                new AnimationOverride(Settings.EmptyActionAnimation, animationData.Clip));
            _characterAnimator.SetFloat(ActionFloatSpeedHash, animationData.AnimationSpeed);
            _characterAnimator.SetTrigger(PlayActionTriggerHash);
        }

        public void PlayActionAnimation(IContinuousActionAnimationData animationData)
        {
            _characterAnimator.ResetTrigger(CancelActionAnimationTriggerHash);
            var animationOverrides = new List<AnimationOverride>
            {
                new(Settings.EmptyPrepareContinuousActionAnimation,
                    animationData.PrepareContinuousActionAnimation.Clip),
                new(Settings.EmptyContinuousActionAnimation, animationData.ContinuousActionAnimation.Clip)
            };
            ApplyAnimationOverride(OverrideController, animationOverrides);
            _characterAnimator.SetFloat(PrepareContinuousActionFloatSpeedHash,
                animationData.PrepareContinuousActionAnimation.AnimationSpeed);
            _characterAnimator.SetFloat(ContinuousActionFloatSpeedHash,
                animationData.ContinuousActionAnimation.AnimationSpeed);
            _characterAnimator.SetTrigger(PlayContinuousActionTriggerHash);
        }

        public void CancelActionAnimation()
        {
            _characterAnimator.SetTrigger(CancelActionAnimationTriggerHash);
        }

        protected void ApplyAnimationOverride(AnimatorOverrideController baseController,
            AnimationOverride animationOverride)
        {
            ApplyAnimationOverride(baseController, new List<AnimationOverride> {animationOverride});
        }

        protected void ApplyAnimationOverride(AnimatorOverrideController baseController,
            List<AnimationOverride> overrides)
        {
            var originalOverrides = new List<KeyValuePair<AnimationClip, AnimationClip>>(baseController.overridesCount);
            baseController.GetOverrides(originalOverrides);

            foreach (var animationOverride in overrides)
            {
                var actionClipOverride =
                    originalOverrides.Find(clipOverride => clipOverride.Key == animationOverride.OriginalClip);
                originalOverrides.Remove(actionClipOverride);
                originalOverrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(animationOverride.OriginalClip,
                    animationOverride.OverrideClip));
            }

            baseController.ApplyOverrides(originalOverrides);

            _characterAnimator.runtimeAnimatorController = baseController;
        }
    }
}