namespace Common.Animation_Data
{
    public interface IContinuousActionAnimationData
    {
        public IAnimationData PrepareContinuousActionAnimation { get; }
        public IAnimationData ContinuousActionAnimation { get; }
    }
}