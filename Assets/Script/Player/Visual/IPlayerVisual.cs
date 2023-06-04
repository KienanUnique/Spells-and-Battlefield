using UnityEngine;

namespace Player.Visual
{
    public interface IPlayerVisual
    {
        void PlayUseSpellAnimation(AnimatorOverrideController useSpellHandsAnimatorController);
        void PlayGroundJumpAnimation();
        void PlayFallAnimation();
        void PlayLandAnimation();
        void PlayDieAnimation();
        void UpdateMovingData(Vector2 movingDirectionNormalized, float ratioOfCurrentVelocityToMaximumVelocity);
    }
}