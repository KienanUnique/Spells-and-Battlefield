using UnityEngine;

namespace Enemies.Look_Point_Calculator.Concrete_Types
{
    public class FollowVelocityDirectionLookPointCalculator : LookPointCalculatorBase
    {
        public override Vector3 CalculateLookPointDirection()
        {
            return _thisRigidbody.Velocity == Vector3.zero
                ? DefaultRotation
                : CurrentVelocity.normalized;
        }
    }
}