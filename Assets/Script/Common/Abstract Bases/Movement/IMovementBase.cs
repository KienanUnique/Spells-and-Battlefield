namespace Common.Abstract_Bases.Movement
{
    public interface IMovementBase
    {
        void MultiplySpeedRatioBy(float speedRatio);
        void DivideSpeedRatioBy(float speedRatio);
    }
}