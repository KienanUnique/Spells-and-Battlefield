using Common.Abstract_Bases.Movement.Coefficients_Calculator;
using Common.Settings.Sections.Movement;

namespace Enemies.Movement
{
    public class EnemyMovementValuesCalculator : MovementValuesCalculatorBase
    {
        public EnemyMovementValuesCalculator(IMovementSettingsSection settings) : base(settings)
        {
        }

        public float MoveForceCalculated => _settings.MoveForce * ExternalSetSpeedRatio;
    }
}