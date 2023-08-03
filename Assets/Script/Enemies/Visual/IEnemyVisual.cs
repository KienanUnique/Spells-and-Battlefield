namespace Enemies.Visual
{
    public interface IEnemyVisual : IEnemyActionAnimationPlayer
    {
        public void UpdateMovingData(bool isRunning);
        public void PlayDieAnimation();
    }
}