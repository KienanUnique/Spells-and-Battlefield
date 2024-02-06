using Common.Animation_Data;
using Common.Animation_Data.Continuous_Action;

namespace Enemies.Visual
{
    public interface IEnemyActionAnimationPlayer
    {
        public void PlayActionAnimation(IAnimationData animationData);
        public void PlayActionAnimation(IContinuousActionAnimationData animationData);
        public void CancelActionAnimation();
    }
}