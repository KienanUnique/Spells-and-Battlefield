using Common.Animation_Data;
using UnityEngine;

namespace Player.Visual
{
    public interface IPlayerVisual
    {
        public void PlayActionAnimation(IAnimationData animationData);
        public void PlayActionAnimation(IContinuousActionAnimationData animationData);
        public void PlayGroundJumpAnimation();
        public void PlayFallAnimation();
        public void PlayLandAnimation();
        public void PlayDieAnimation();
        public void CancelActionAnimation();
        public void UpdateMovingData(Vector2 movingDirectionNormalized, float ratioOfCurrentVelocityToMaximumVelocity);
    }
}