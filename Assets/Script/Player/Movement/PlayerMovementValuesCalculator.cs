using System;
using Common.Abstract_Bases.Movement.Coefficients_Calculator;
using Common.Readonly_Rigidbody;
using Player.Movement.Settings;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovementValuesCalculator : MovementValuesCalculatorBase
    {
        private readonly IPlayerMovementSettings _playerMovementSettings;
        private readonly IReadonlyRigidbody _readonlyRigidbody;
        private float _currentFrictionCoefficient;
        private float _currentPlayerInputForceMultiplier;
        private float _currentGravityForceMultiplier;

        public PlayerMovementValuesCalculator(IPlayerMovementSettings movementSettings,
            IReadonlyRigidbody readonlyRigidbody) : base(movementSettings)
        {
            _playerMovementSettings = movementSettings;
            _readonlyRigidbody = readonlyRigidbody;
        }

        public float GravityForce => _playerMovementSettings.NormalGravityForce * _currentGravityForceMultiplier;
        public float DashForce => _playerMovementSettings.DashForce;
        private float JumpForce => _playerMovementSettings.JumpForce;

        public void ChangeFrictionCoefficient(float newFrictionCoefficient)
        {
            _currentFrictionCoefficient = newFrictionCoefficient;
        }

        public void ChangePlayerInputForceMultiplier(float newPlayerInputForceMultiplier)
        {
            _currentPlayerInputForceMultiplier = newPlayerInputForceMultiplier;
        }

        public void ChangeGravityForceMultiplier(float newGravityForceMultiplier)
        {
            _currentGravityForceMultiplier = newGravityForceMultiplier;
        }

        public Vector3 CalculateMoveForce(Vector2 inputMoveDirection)
        {
            return _settings.MoveForce *
                   _currentPlayerInputForceMultiplier *
                   ExternalSetSpeedRatio *
                   (inputMoveDirection.x * _readonlyRigidbody.Right +
                    inputMoveDirection.y * _currentPlayerInputForceMultiplier * _readonlyRigidbody.Forward);
        }

        public Vector3 CalculateJumpForce()
        {
            return JumpForce * Vector3.up;
        }

        public Vector3 CalculateJumpForce(WallDirection wallDirection)
        {
            Vector3 needDirection = wallDirection switch
            {
                WallDirection.Left => _readonlyRigidbody.Right,
                WallDirection.Right => _readonlyRigidbody.Right * -1,
                _ => throw new ArgumentOutOfRangeException(nameof(wallDirection), wallDirection, null)
            };

            needDirection = RotateTowardsUp(needDirection, _playerMovementSettings.WallRunningJumpAngleTowardsUp);

            return needDirection * JumpForce;
        }

        public Vector3 CalculateFrictionForce(Vector2 inputMoveDirection)
        {
            Vector3 inversedVelocity = -_readonlyRigidbody.InverseTransformDirection(_readonlyRigidbody.Velocity);
            Vector3 finalFrictionForce = Vector3.zero;
            if (inputMoveDirection.x == 0)
            {
                finalFrictionForce += inversedVelocity.x * _readonlyRigidbody.Right;
            }

            if (inputMoveDirection.y == 0)
            {
                finalFrictionForce += inversedVelocity.z * _readonlyRigidbody.Forward;
            }

            finalFrictionForce *= _settings.MoveForce * _currentFrictionCoefficient;

            return finalFrictionForce;
        }

        private static Vector3 RotateTowardsUp(Vector3 start, float angle)
        {
            Vector3 axis = Vector3.Cross(start, Vector3.up);
            if (axis == Vector3.zero)
            {
                axis = Vector3.right;
            }

            return Quaternion.AngleAxis(angle, axis) * start;
        }
    }
}