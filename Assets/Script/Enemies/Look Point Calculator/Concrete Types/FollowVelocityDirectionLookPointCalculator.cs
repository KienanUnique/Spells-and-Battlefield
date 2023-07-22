using UnityEngine;

namespace Enemies.Look_Point_Calculator.Concrete_Types
{
    public class FollowVelocityDirectionLookPointCalculator : LookPointCalculatorBase
    {
        public override Quaternion CalculateLookPointDirection()
        {
            return _thisRigidbody.Velocity == Vector3.zero
                ? _thisRigidbody.Rotation
                : Quaternion.LookRotation(_thisRigidbody.Velocity);
        }
    }
}