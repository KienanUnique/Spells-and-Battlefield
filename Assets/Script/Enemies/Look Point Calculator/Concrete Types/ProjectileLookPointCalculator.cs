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

        public override Quaternion CalculateLookPointDirection()
        {
            if (_isTargetNull)
            {
                return Quaternion.identity;
            }

            var direction =
                (TargetRigidbody.Position + TargetRigidbody.Velocity *
                    Vector3.Distance(TargetRigidbody.Position, _thisRigidbody.Position) / _projectileSpeed)
                - _thisRigidbody.Position;
            direction.Normalize();
            return Quaternion.LookRotation(direction);
        }
    }
}