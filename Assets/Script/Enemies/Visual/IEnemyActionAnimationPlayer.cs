using Common.Animation_Data;

namespace Enemies.Visual
{
    public interface IEnemyActionAnimationPlayer
    {
        public void PlayActionAnimation(IAnimationData animationData);
    }
}