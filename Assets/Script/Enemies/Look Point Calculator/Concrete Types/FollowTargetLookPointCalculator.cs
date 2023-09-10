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

            Vector3 direction = CurrentTargetPosition - CurrentPosition;
            direction.Normalize();
            return direction;
        }
    }
}