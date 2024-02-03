using Common.Abstract_Bases.Visual;
using Common.Abstract_Bases.Visual.Settings;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player.Visual
{
    public class PlayerVisual : VisualBase, IPlayerVisual 
    {
        private static readonly int MovingDirectionXFloatHash = Animator.StringToHash("Moving Direction X");
        private static readonly int MovingDirectionYFloatHash = Animator.StringToHash("Moving Direction Y");

        private static readonly int RatioOfCurrentVelocityToMaximumVelocityFloatHash =
            Animator.StringToHash("Ratio Of Current Velocity To Maximum Velocity");

        private static readonly int JumpTriggerHash = Animator.StringToHash("Jump");
        private static readonly int FallTriggerHash = Animator.StringToHash("Fall");
        private static readonly int LandTriggerHash = Animator.StringToHash("Land");
        private static readonly int DieTriggerHash = Animator.StringToHash("Die");
        private static readonly int IsHookingHash = Animator.StringToHash("Is Hooking");
        private static readonly int HookPushingStartedHash = Animator.StringToHash("Hook Pushing Started");

        public PlayerVisual(RigBuilder rigBuilder, Animator characterAnimator, IVisualSettings settings) : base(
            rigBuilder, characterAnimator)
        {
            Settings = settings;
            OverrideController = new AnimatorOverrideController(_characterAnimator.runtimeAnimatorController);
        }

        protected override IVisualSettings Settings { get; }
        protected override AnimatorOverrideController OverrideController { get; }

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
            _characterAnimator.ResetTrigger(HookPushingStartedHash);
            _characterAnimator.SetTrigger(DieTriggerHash);
        }

        public void StartPlayingHookAnimation()
        {
            _characterAnimator.SetBool(IsHookingHash, true);
        }

        public void PlayHookPushingAnimation()
        {
            _characterAnimator.SetTrigger(HookPushingStartedHash);
        }

        public void StopPlayingHookAnimation()
        {
            _characterAnimator.ResetTrigger(JumpTriggerHash);
            _characterAnimator.ResetTrigger(FallTriggerHash);
            _characterAnimator.ResetTrigger(LandTriggerHash);
            _characterAnimator.ResetTrigger(HookPushingStartedHash);
            _characterAnimator.SetBool(IsHookingHash, false);
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