using UnityEngine;

namespace Enemies.Look_Point_Calculator.Concrete_Types
{
    public class FollowTargetLookPointCalculator : LookPointCalculatorBase
    {
        public override Quaternion CalculateLookPointDirection()
        {
            if (_isTargetNull)
            {
                return Quaternion.identity;
            }

            var direction = TargetRigidbody.Position - _thisRigidbody.Position;
            direction.Normalize();
            return Quaternion.LookRotation(direction);
        }
    }
}