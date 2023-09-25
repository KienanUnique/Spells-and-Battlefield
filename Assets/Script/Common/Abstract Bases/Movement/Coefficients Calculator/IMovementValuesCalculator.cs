using Common.Mechanic_Effects.Concrete_Types.Change_Speed;

namespace Common.Abstract_Bases.Movement.Coefficients_Calculator
{
    public interface IMovementValuesCalculator : IMovable
    {
        public float MaximumSpeedCalculated { get; }
        public float CalculateFrictionForce(float magnitude);
    }
}