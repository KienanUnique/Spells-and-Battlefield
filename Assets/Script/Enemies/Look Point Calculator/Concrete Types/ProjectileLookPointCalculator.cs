using UnityEngine;

namespace Enemies.Look_Point_Calculator.Concrete_Types
{
    public class ProjectileLookPointCalculator : LookPointCalculatorBase
    {
        private readonly float _projectileSpeed;

        public ProjectileLookPointCalculator(float projectileSpeed)
        {
            _projectileSpeed = projectileSpeed;
        }

        public override Vector3 CalculateLookPointDirection()
        {
            if (_isTargetNull)
            {
                return DefaultRotation;
            }

            Vector3 direction = TargetRigidbody.Position +
                                TargetRigidbody.Velocity *
                                Vector3.Distance(TargetRigidbody.Position, CurrentPosition) /
                                _projectileSpeed -
                                CurrentPosition;
            return direction.normalized;
        }
    }
}