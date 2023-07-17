using UnityEngine;

namespace Enemies.Look_Point_Calculator.Concrete_Types
{
    public class FollowVelocityDirectionLookPointCalculator : LookPointCalculatorBase
    {
        public override Quaternion CalculateLookPointDirection()
        {
            return Quaternion.LookRotation(_thisRigidbody.Velocity.normalized);
        }
    }
}