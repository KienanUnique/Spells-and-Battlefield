using UnityEngine;

namespace Enemies.Look_Point_Calculator.Concrete_Types
{
    public class KeepLookDirectionLookPointCalculator : LookPointCalculatorBase
    {
        public override Vector3 CalculateLookPointDirection()
        {
            return DefaultRotation;
        }
    }
}