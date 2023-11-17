using System.Collections.Generic;
using Common.Abstract_Bases.Visual;
using Common.Animation_Data;
using Player.Visual.Settings;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player.Visual
{
    public class PlayerVisual : VisualBase, IPlayerVisual
    {
        private static readonly int CancelActionAnimationTriggerHash = Animator.StringToHash("Cancel Action");
        private static readonly int PlayContinuousActionTriggerHash = Animator.StringToHash("Play Continuous Action");

        private static readonly int PrepareContinuousActionFloatSpeedHash =
            Animator.StringToHash("Prepare Continuous Action Speed");

        private static readonly int ContinuousActionFloatSpeedHash = Animator.StringToHash("Continuous Action Speed");

        private static readonly int PlayActionTriggerHash = Animator.StringToHash("Play Action");
        private static readonly int ActionFloatSpeedHash = Animator.StringToHash("Action Speed");

        private static readonly int MovingDirectionXFloatHash = Animator.StringToHash("Moving Direction X");
        private static readonly int MovingDirectionYFloatHash = Animator.StringToHash("Moving Direction Y");

        private static readonly int RatioOfCurrentVelocityToMaximumVelocityFloatHash =
            Animator.StringToHash("Ratio Of Current Velocity To Maximum Velocity");

        private static readonly int JumpTriggerHash = Animator.StringToHash("Jump");
        private static readonly int FallTriggerHash = Animator.StringToHash("Fall");
        private static readonly int LandTriggerHash = Animator.StringToHash("Land");
        private static readonly int DieTriggerHash = Animator.StringToHash("Die");
        private readonly IPlayerVisualSettings _settings;

        public PlayerVisual(RigBuilder rigBuilder, Animator characterAnimator, IPlayerVisualSettings settings) : base(
            rigBuilder, characterAnimator)
        {
            _settings = settings;
        }

        public void PlayActionAnimation(IAnimationData animationData)
        {
            ApplyAnimationOverride(new AnimatorOverrideController(_characterAnimator.runtimeAnimatorController),
                new AnimationOverride(_settings.EmptyActionAnimation, animationData.Clip));
            _characterAnimator.SetFloat(ActionFloatSpeedHash, animationData.AnimationSpeed);
            _characterAnimator.SetTrigger(PlayActionTriggerHash);
        }

        public void PlayActionAnimation(IContinuousActionAnimationData animationData)
        {
            var animationOverrides = new List<AnimationOverride>
            {
                new AnimationOverride(_settings.EmptyPrepareContinuousActionAnimation,
                    animationData.PrepareContinuousActionAnimation.Clip),
                new AnimationOverride(_settings.EmptyContinuousActionAnimation,
                    animationData.ContinuousActionAnimation.Clip)
            };
            ApplyAnimationOverride(new AnimatorOverrideController(_characterAnimator.runtimeAnimatorController),
                animationOverrides);
            _characterAnimator.SetFloat(PrepareContinuousActionFloatSpeedHash,
                animationData.PrepareContinuousActionAnimation.AnimationSpeed);
            _characterAnimator.SetFloat(ContinuousActionFloatSpeedHash,
                animationData.ContinuousActionAnimation.AnimationSpeed);
            _characterAnimator.SetTrigger(PlayContinuousActionTriggerHash);
        }

        public void PlayGroundJumpAnimation()
        {
            _characterAnimator.SetTrigger(JumpTriggerHash);
            _characterAnimator.ResetTrigger(FallTriggerHash);
            _characterAnimator.ResetTrigger(LandTriggerHash);
        }

        public void PlayFallAnimation()
        {
            _characterAnimator.ResetTrigger(JumpTriggerHash);
            _characterAnimator.SetTrigger(FallTriggerHash);
            _characterAnimator.ResetTrigger(LandTriggerHash);
        }

        public void PlayLandAnimation()
        {
            _characterAnimator.ResetTrigger(JumpTriggerHash);
            _characterAnimator.ResetTrigger(FallTriggerHash);
            _characterAnimator.SetTrigger(LandTriggerHash);
        }

        public void PlayDieAnimation()
        {
            _rigBuilder.enabled = false;
            _characterAnimator.ResetTrigger(JumpTriggerHash);
            _characterAnimator.ResetTrigger(FallTriggerHash);
            _characterAnimator.ResetTrigger(LandTriggerHash);
            _characterAnimator.SetTrigger(DieTriggerHash);
        }

        public void CancelActionAnimation()
        {
            _characterAnimator.SetTrigger(CancelActionAnimationTriggerHash);
        }

        public void UpdateMovingData(Vector2 movingDirectionNormalized, float ratioOfCurrentVelocityToMaximumVelocity)
        {
            _characterAnimator.SetFloat(MovingDirectionXFloatHash, movingDirectionNormalized.x);
            _characterAnimator.SetFloat(MovingDirectionYFloatHash, movingDirectionNormalized.y);
            _characterAnimator.SetFloat(RatioOfCurrentVelocityToMaximumVelocityFloatHash,
                ratioOfCurrentVelocityToMaximumVelocity);
        }
    }
}