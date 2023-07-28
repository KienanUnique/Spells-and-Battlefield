using Common.Animation_Data;

namespace Enemies.Visual
{
    public interface IEnemyVisual
    {
        public void UpdateMovingData(bool isRunning);
        public void PlayDieAnimation();
        public void StartPlayingActionAnimation(IAnimationData animationData);
        public void StopPlayingActionAnimation();
    }
}