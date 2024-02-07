using Common.Abstract_Bases.Movement.Coefficients_Calculator;
using UnityEngine;

namespace Player.Movement
{
    public interface IPlayerMovementValuesCalculator : IMovementValuesCalculator
    {
        public bool IsInputMovingEnabled { get; }
        public float GravityForce { get; }
        public float BaseMaximumSpeed { get; }
        public float DashForce { get; }
        public Vector3 MoveForce { get; }
        public Vector3 FrictionForce { get; }
        public float CurrentOverSpeedingValue { get; }
        public void ChangeFrictionCoefficient(float newFrictionCoefficient);
        public void ChangePlayerInputForceMultiplier(float newPlayerInputForceMultiplier);
        public void ChangeGravityForceMultiplier(float newGravityForceMultiplier);
        public void IncreaseAdditionalMaximumSpeed(float changeSpeed, float endValue);
        public void DecreaseAdditionalMaximumSpeed(float changeSpeed);
        public Vector3 CalculateJumpForce();
        public Vector3 CalculateJumpForce(WallDirection wallDirection);
        public void UpdateMoveInput(Vector2 direction2d);
        public Vector3 CalculateHookForce(Vector3 hookerHookPushDirection);
        public void EnableInput();
        public void DisableInput();
    }
}