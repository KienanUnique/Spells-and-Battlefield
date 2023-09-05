using Common.Abstract_Bases.Visual;
using Common.Animation_Data;
using Player.Settings;
using Player.Visual.Settings;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Player.Visual
{
    public class PlayerVisual : VisualBase, IPlayerVisual
    {
        private static readonly int AttackTriggerHash = Animator.StringToHash("Attack");
        private static readonly int UseSpellFloatSpeedHash = Animator.StringToHash("Use Spell Speed");
        private static readonly int MovingDirectionXFloatHash = Animator.StringToHash("Moving Direction X");
        private static readonly int MovingDirectionYFloatHash = Animator.StringToHash("Moving Direction Y");

        private static readonly int RatioOfCurrentVelocityToMaximumVelocityFloatHash =
            Animator.StringToHash("Ratio Of Current Velocity To Maximum Velocity");

        private static readonly int JumpTriggerHash = Animator.StringToHash("Jump");
        private static readonly int FallTriggerHash = Animator.StringToHash("Fall");
        private static readonly int LandTriggerHash = Animator.StringToHash("Land");
        private static readonly int DieTriggerHash = Animator.StringToHash("Die");
        private readonly IPlayerVisualSettings _settings;

        public PlayerVisual(RigBuilder rigBuilder, Animator characterAnimator, IPlayerVisualSettings settings) :
            base(rigBuilder, characterAnimator)
        {
            _settings = settings;
        }

        public void PlayUseSpellAnimation(IAnimationData spellAnimationData)
        {
            ApplyAnimationOverride(new AnimatorOverrideController(_characterAnimator.runtimeAnimatorController),
                _settings.EmptyUseSpellAnimation,
                spellAnimationData.Clip);
            _characterAnimator.SetFloat(UseSpellFloatSpeedHash, spellAnimationData.AnimationSpeed);
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