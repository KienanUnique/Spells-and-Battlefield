namespace Common.Mechanic_Effects.Concrete_Types.Change_Speed
{
    public interface IMovable
    {
        public void MultiplySpeedRatioBy(float speedRatio);
        public void DivideSpeedRatioBy(float speedRatio);
    }
}