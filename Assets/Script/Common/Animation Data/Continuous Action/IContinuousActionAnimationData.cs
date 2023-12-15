namespace Common.Animation_Data.Continuous_Action
{
    public interface IContinuousActionAnimationData
    {
        public IAnimationData PrepareContinuousActionAnimation { get; }
        public IAnimationData ContinuousActionAnimation { get; }
    }
}