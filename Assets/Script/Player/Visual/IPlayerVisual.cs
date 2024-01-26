using Common.Animation_Data;
using Common.Animation_Data.Continuous_Action;
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
        public void StartPlayingHookAnimation();
        public void PlayHookPushingAnimation();
        public void StopPlayingHookAnimation();
        public void CancelActionAnimation();
        public void UpdateMovingData(Vector2 movingDirectionNormalized, float ratioOfCurrentVelocityToMaximumVelocity);
    }
}