using Common.Animation_Data;
using UnityEngine;

namespace Player.Visual
{
    public interface IPlayerVisual
    {
        void PlayUseSpellAnimation(IAnimationData spellAnimationData);
        void PlayGroundJumpAnimation();
        void PlayFallAnimation();
        void PlayLandAnimation();
        void PlayDieAnimation();
        void UpdateMovingData(Vector2 movingDirectionNormalized, float ratioOfCurrentVelocityToMaximumVelocity);
    }
}