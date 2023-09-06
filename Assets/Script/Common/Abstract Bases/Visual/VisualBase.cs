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

        protected void ApplyAnimationOverride(AnimatorOverrideController baseController, AnimationClip originalClip,
            AnimationClip newClip)
        {
            var originalOverrides = new List<KeyValuePair<AnimationClip, AnimationClip>>(baseController.overridesCount);
            baseController.GetOverrides(originalOverrides);

            KeyValuePair<AnimationClip, AnimationClip> actionClipOverride =
                originalOverrides.Find(clipOverride => clipOverride.Key == originalClip);
            originalOverrides.Remove(actionClipOverride);
            originalOverrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(originalClip, newClip));

            baseController.ApplyOverrides(originalOverrides);

            _characterAnimator.runtimeAnimatorController = baseController;
        }
    }
}