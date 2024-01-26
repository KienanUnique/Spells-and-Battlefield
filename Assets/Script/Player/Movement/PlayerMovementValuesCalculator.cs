using System;
using System.Collections;
using Common.Abstract_Bases.Movement.Coefficients_Calculator;
using Common.Interfaces;
using Common.Readonly_Rigidbody;
using DG.Tweening;
using Player.Movement.Settings;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovementValuesCalculator : MovementValuesCalculatorBase, IPlayerMovementValuesCalculator
    {
        private const float MinimumLocalAdditionalMaximumSpeed = 0f;
        private const float FloatComparisonTolerance = 0.001f;
        private readonly IPlayerMovementSettings _playerMovementSettings;
        private readonly IReadonlyRigidbody _readonlyRigidbody;
        private float _currentFrictionCoefficient;
        private float _currentPlayerInputForceMultiplier;
        private float _currentGravityForceMultiplier;
        private float _localAdditionalMaximumSpeed;
        private Tweener _currentLocalAdditionalMaximumSpeedChangeTween;
        private Tweener _currentLocalAdditionalMaximumSpeedResetTween;
        private Vector2 _inputMoveDirection = Vector2.zero;
        private float _currentOverSpeedRatio;

        public PlayerMovementValuesCalculator(IPlayerMovementSettings movementSettings,
            IReadonlyRigidbody readonlyRigidbody, ICoroutineStarter coroutineStarter) : base(movementSettings)
        {
            _playerMovementSettings = movementSettings;
            _readonlyRigidbody = readonlyRigidbody;
            coroutineStarter.StartCoroutine(HandleInputMovement());
        }

        public override float MaximumSpeedCalculated =>
            (_settings.MaximumSpeed + _localAdditionalMaximumSpeed) * ExternalSetSpeedRatio;

        public float GravityForce => _playerMovementSettings.NormalGravityForce * _currentGravityForceMultiplier;

        public float BaseMaximumSpeed => _settings.MaximumSpeed;

        public float DashForce => _playerMovementSettings.DashForce;

        public Vector3 MoveForce =>
            _settings.MoveForce *
            _currentPlayerInputForceMultiplier *
            ExternalSetSpeedRatio *
            (_inputMoveDirection.x * _readonlyRigidbody.Right +
             _inputMoveDirection.y * _currentPlayerInputForceMultiplier * _readonlyRigidbody.Forward);

        public Vector3 FrictionForce
        {
            get
            {
                Vector3 inversedVelocity = -_readonlyRigidbody.InverseTransformDirection(_readonlyRigidbody.Velocity);
                Vector3 finalFrictionForce = Vector3.zero;
                if (_inputMoveDirection.x == 0)
                {
                    finalFrictionForce += inversedVelocity.x * _readonlyRigidbody.Right;
                }

                if (_inputMoveDirection.y == 0)
                {
                    finalFrictionForce += inversedVelocity.z * _readonlyRigidbody.Forward;
                }

                finalFrictionForce *= _settings.MoveForce * _currentFrictionCoefficient;

                return finalFrictionForce;
            }
        }

        public float CurrentOverSpeedingValue
        {
            get
            {
                float overSpeedValue = _readonlyRigidbody.Velocity.magnitude - BaseMaximumSpeed;
                if (overSpeedValue < 0)
                {
                    overSpeedValue = 0;
                }

                return overSpeedValue;
            }
        }

        private float BaseJumpForce => _playerMovementSettings.JumpForce;

        private static Vector3 RotateTowardsUp(Vector3 start, float angle)
        {
            Vector3 axis = Vector3.Cross(start, Vector3.up);
            if (axis == Vector3.zero)
            {
                axis = Vector3.right;
            }

            return Quaternion.AngleAxis(angle, axis) * start;
        }

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

        public void IncreaseAdditionalMaximumSpeed(float changeSpeed, float endValue)
        {
            ChangeAdditionalMaximumSpeed(changeSpeed, endValue);
        }

        public void DecreaseAdditionalMaximumSpeed(float changeSpeed)
        {
            ChangeAdditionalMaximumSpeed(changeSpeed, MinimumLocalAdditionalMaximumSpeed);
        }

        public Vector3 CalculateJumpForce()
        {
            return BaseJumpForce * Vector3.up;
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

            return needDirection * BaseJumpForce;
        }

        public void UpdateMoveInput(Vector2 direction2d)
        {
            _inputMoveDirection = direction2d;
        }

        public Vector3 CalculateHookForce(Vector3 hookerHookPushDirection)
        {
            return hookerHookPushDirection * _settings.HookForce;
        }

        private void ChangeAdditionalMaximumSpeed(float changeSpeed, float endValue)
        {
            if (_currentLocalAdditionalMaximumSpeedChangeTween.IsActive())
            {
                _currentLocalAdditionalMaximumSpeedChangeTween.Kill();
            }

            _currentLocalAdditionalMaximumSpeedChangeTween = DOTween.To(ApplyLocalAdditionalMaximumSpeedChange,
                                                                        _localAdditionalMaximumSpeed, endValue,
                                                                        changeSpeed)
                                                                    .SetSpeedBased()
                                                                    .SetUpdate(UpdateType.Fixed);
        }

        private IEnumerator HandleInputMovement()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                if (_inputMoveDirection == Vector2.zero)
                {
                    TryResetAdditionalMaximumSpeed();
                }
                else
                {
                    TryContinueChangingAdditionalMaximumSpeed();
                }

                yield return waitForFixedUpdate;
            }
        }

        private void TryResetAdditionalMaximumSpeed()
        {
            if (_currentLocalAdditionalMaximumSpeedChangeTween != null &&
                _currentLocalAdditionalMaximumSpeedChangeTween.IsActive() &&
                !_currentLocalAdditionalMaximumSpeedChangeTween.IsComplete())
            {
                _currentLocalAdditionalMaximumSpeedChangeTween.Pause();
            }

            if (_currentLocalAdditionalMaximumSpeedResetTween != null &&
                _currentLocalAdditionalMaximumSpeedResetTween.IsActive() &&
                !_currentLocalAdditionalMaximumSpeedResetTween.IsComplete())
            {
                _currentLocalAdditionalMaximumSpeedResetTween.Kill();
                _currentLocalAdditionalMaximumSpeedResetTween = null;
            }

            if (Math.Abs(_localAdditionalMaximumSpeed - MinimumLocalAdditionalMaximumSpeed) < FloatComparisonTolerance)
            {
                return;
            }

            _currentLocalAdditionalMaximumSpeedResetTween = DOTween.To(ApplyLocalAdditionalMaximumSpeedChange,
                                                                       _localAdditionalMaximumSpeed,
                                                                       MinimumLocalAdditionalMaximumSpeed,
                                                                       _playerMovementSettings
                                                                           .NoInputMovingDecreaseAdditionalMaximumSpeedAcceleration)
                                                                   .SetSpeedBased()
                                                                   .SetUpdate(UpdateType.Fixed);
        }

        private void TryContinueChangingAdditionalMaximumSpeed()
        {
            if (_currentLocalAdditionalMaximumSpeedResetTween != null &&
                _currentLocalAdditionalMaximumSpeedResetTween.IsActive() &&
                !_currentLocalAdditionalMaximumSpeedResetTween.IsComplete())
            {
                _currentLocalAdditionalMaximumSpeedResetTween.Kill();
                _currentLocalAdditionalMaximumSpeedResetTween = null;
            }

            if (_currentLocalAdditionalMaximumSpeedChangeTween != null &&
                _currentLocalAdditionalMaximumSpeedChangeTween.IsActive() &&
                !_currentLocalAdditionalMaximumSpeedChangeTween.IsComplete())
            {
                _currentLocalAdditionalMaximumSpeedChangeTween.Play();
            }
        }

        private void ApplyLocalAdditionalMaximumSpeedChange(float newLocalAdditionalMaximumSpeed)
        {
            _localAdditionalMaximumSpeed = newLocalAdditionalMaximumSpeed;
        }
    }
}