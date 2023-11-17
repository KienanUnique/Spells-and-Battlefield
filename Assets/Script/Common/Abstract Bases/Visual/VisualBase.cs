using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Common.Abstract_Bases.Visual
{
    public abstract class VisualBase
    {
        protected readonly Animator _characterAnimator;
        protected readonly RigBuilder _rigBuilder;

        protected VisualBase(RigBuilder rigBuilder, Animator characterAnimator)
        {
            _rigBuilder = rigBuilder;
            _characterAnimator = characterAnimator;
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

            foreach (AnimationOverride animationOverride in overrides)
            {
                KeyValuePair<AnimationClip, AnimationClip> actionClipOverride =
                    originalOverrides.Find(clipOverride => clipOverride.Key == animationOverride.OriginalClip);
                originalOverrides.Remove(actionClipOverride);
                originalOverrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(animationOverride.OriginalClip,
                    animationOverride.OverrideClip));
            }

            baseController.ApplyOverrides(originalOverrides);

            _characterAnimator.runtimeAnimatorController = baseController;
        }

        protected struct AnimationOverride
        {
            public AnimationOverride(AnimationClip originalClip, AnimationClip overrideClip)
            {
                OriginalClip = originalClip;
                OverrideClip = overrideClip;
            }

            public AnimationClip OriginalClip { get; }
            public AnimationClip OverrideClip { get; }
        }
    }
}