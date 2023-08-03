using UnityEngine;

namespace Enemies.Look_Point_Calculator.Concrete_Types
{
    public class FollowTargetLookPointCalculator : LookPointCalculatorBase
    {
        public override Vector3 CalculateLookPointDirection()
        {
            if (_isTargetNull)
            {
                return DefaultRotation;
            }

            var direction = TargetRigidbody.Position - _thisRigidbody.Position;
            direction.Normalize();
            return direction;
        }
    }
}