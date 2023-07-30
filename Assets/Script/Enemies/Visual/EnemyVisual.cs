using System.Collections.Generic;
using System.Linq;
using Common.Animation_Data;
using UnityEngine;

namespace Enemies.Visual
{
    public class EnemyVisual : IEnemyVisual
    {
        private readonly Animator _characterAnimator;
        private static readonly int ActionBoolHash = Animator.StringToHash("Is Doing Action");
        private static readonly int ActionFloatAnimationSpeedHash = Animator.StringToHash("Use Action Speed");
        private static readonly int IsRunningBoolHash = Animator.StringToHash("Is Running");
        private static readonly int DieTriggerHash = Animator.StringToHash("Die");
        private readonly AnimatorOverrideController _baseAnimatorOverrideController;

        public EnemyVisual(Animator characterAnimator, AnimatorOverrideController baseAnimatorOverrideController)
        {
            _characterAnimator = characterAnimator;
            _baseAnimatorOverrideController = baseAnimatorOverrideController;
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

        public void StartPlayingActionAnimation(IAnimationData animationData)
        {
            var originalOverrides =
                new List<KeyValuePair<AnimationClip, AnimationClip>>(animationData.AnimationAnimatorOverrideController
                    .overridesCount);
            _baseAnimatorOverrideController.GetOverrides(originalOverrides);
            PrintAnimationsData(originalOverrides);

            var newOverrides =
                new List<KeyValuePair<AnimationClip, AnimationClip>>(animationData.AnimationAnimatorOverrideController
                    .overridesCount);
            animationData.AnimationAnimatorOverrideController.GetOverrides(newOverrides);
            Debug.Log("=================");
            PrintAnimationsData(newOverrides);

            foreach (var animationOverride in
                     newOverrides.Where(clipOverride => clipOverride.Value != null))
            {
                var indexOfClipNeedsOverride =
                    originalOverrides.FindIndex(clipOverride => clipOverride.Key == animationOverride.Key);
                if (indexOfClipNeedsOverride != -1)
                {
                    originalOverrides[indexOfClipNeedsOverride] = animationOverride;
                }
            }

            var needController = new AnimatorOverrideController(_baseAnimatorOverrideController);
            needController.ApplyOverrides(originalOverrides);

            ApplyRuntimeAnimatorController(needController);
            _characterAnimator.SetFloat(ActionFloatAnimationSpeedHash, animationData.AnimationSpeed);
            _characterAnimator.SetBool(ActionBoolHash, true);
        }

        private void PrintAnimationsData(List<KeyValuePair<AnimationClip, AnimationClip>> animations)
        {
            foreach (var keyValuePair in animations)
            {
                Debug.Log(keyValuePair.Value != null
                    ? $"{keyValuePair.Key.name} {keyValuePair.Value.name}"
                    : $"{keyValuePair.Key.name} null");
            }
        }

        public void StopPlayingActionAnimation()
        {
            _characterAnimator.SetBool(ActionBoolHash, false);
        }

        private void ApplyRuntimeAnimatorController(RuntimeAnimatorController animatorOverrideController)
        {
            _characterAnimator.runtimeAnimatorController = animatorOverrideController;
        }
    }
}