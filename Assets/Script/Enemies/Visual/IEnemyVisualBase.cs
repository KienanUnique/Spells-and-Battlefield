namespace Enemies.Visual
{
    public interface IEnemyVisualBase
    {
        void UpdateMovingData(bool isRunning);
        void PlayDieAnimation();
    }
}