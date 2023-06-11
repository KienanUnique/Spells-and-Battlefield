using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player.Visual
{
    public class PlayerVisual : IPlayerVisual
    {
        private static readonly int AttackTriggerHash = Animator.StringToHash("Attack");
        private static readonly int MovingDirectionXFloatHash = Animator.StringToHash("Moving Direction X");
        private static readonly int MovingDirectionYFloatHash = Animator.StringToHash("Moving Direction Y");

        private static readonly int RatioOfCurrentVelocityToMaximumVelocityFloatHash =
            Animator.StringToHash("Ratio Of Current Velocity To Maximum Velocity");

        private static readonly int JumpTriggerHash = Animator.StringToHash("Jump");
        private static readonly int FallTriggerHash = Animator.StringToHash("Fall");
        private static readonly int LandTriggerHash = Animator.StringToHash("Land");
        private static readonly int DieTriggerHash = Animator.StringToHash("Die");

        private readonly RigBuilder _rigBuilder;
        private readonly Animator _characterAnimator;

        public PlayerVisual(RigBuilder rigBuilder, Animator characterAnimator)
        {
            _rigBuilder = rigBuilder;
            _characterAnimator = characterAnimator;
        }

        public void PlayUseSpellAnimation(AnimatorOverrideController useSpellHandsAnimatorController)
        {
            _characterAnimator.runtimeAnimatorController = useSpellHandsAnimatorController;
            _characterAnimator.SetTrigger(AttackTriggerHash);
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

        public void UpdateMovingData(Vector2 movingDirectionNormalized, float ratioOfCurrentVelocityToMaximumVelocity)
        {
            _characterAnimator.SetFloat(MovingDirectionXFloatHash, movingDirectionNormalized.x);
            _characterAnimator.SetFloat(MovingDirectionYFloatHash, movingDirectionNormalized.y);
            _characterAnimator.SetFloat(RatioOfCurrentVelocityToMaximumVelocityFloatHash,
                ratioOfCurrentVelocityToMaximumVelocity);
        }
    }
}