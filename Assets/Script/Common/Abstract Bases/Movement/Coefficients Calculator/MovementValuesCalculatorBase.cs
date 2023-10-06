using Common.Settings.Sections.Movement;

namespace Common.Abstract_Bases.Movement.Coefficients_Calculator
{
    public abstract class MovementValuesCalculatorBase : IMovementValuesCalculator
    {
        protected readonly IMovementSettingsSection _settings;

        protected MovementValuesCalculatorBase(IMovementSettingsSection settings)
        {
            _settings = settings;
        }

        public virtual float MaximumSpeedCalculated => _settings.MaximumSpeed * ExternalSetSpeedRatio;
        protected float ExternalSetSpeedRatio { get; private set; } = 1f;

        public void MultiplySpeedRatioBy(float speedRatio)
        {
            ExternalSetSpeedRatio *= speedRatio;
        }

        public void DivideSpeedRatioBy(float speedRatio)
        {
            ExternalSetSpeedRatio /= speedRatio;
        }

        public float CalculateFrictionForce(float magnitude)
        {
            return _settings.NormalFrictionCoefficient * _settings.MoveForce * magnitude;
        }
    }
}