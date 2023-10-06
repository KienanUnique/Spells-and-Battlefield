using System;
using System.Collections;
using Common;
using Common.Abstract_Bases.Checkers.Ground_Checker;
using Common.Abstract_Bases.Checkers.Wall_Checker;
using Common.Abstract_Bases.Movement;
using Common.Abstract_Bases.Movement.Coefficients_Calculator;
using Common.Interfaces;
using Common.Readonly_Rigidbody;
using Player.Movement.Settings;
using UnityEngine;

namespace Player.Movement
{
    public sealed class PlayerMovement : MovementBase, IPlayerMovement
    {
        private const int MaxCountOfAirJumps = 1;
        private const float AirPlayerInputForceMultiplier = 0.5f;
        private const float NormalPlayerInputForceMultiplier = 1;
        private const float WallRunningPlayerInputForceMultiplier = 1.5f;
        private const float DashAimingPlayerInputForceMultiplier = 0;

        private const RigidbodyConstraints RigidbodyConstraintsFreezeRotationAndPosition =
            RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        private readonly Transform _cashedTransform;
        private readonly ICoroutineStarter _coroutineStarter;
        private readonly ValueWithReactionOnChange<MovingState> _currentMovingState;
        private readonly ValueWithReactionOnChange<WallDirection> _currentWallDirection;
        private readonly IGroundChecker _groundChecker;
        private readonly IPlayerMovementSettings _movementSettings;
        private readonly IWallChecker _wallChecker;
        private readonly Transform _originalParent;
        private readonly CoyoteTimeWaiter _airCoyoteTimeWaiter;
        private readonly IPlayerMovementValuesCalculator _movementValuesCalculator;

        private bool _canDash = true;
        private int _currentCountOfAirJumps;
        private Coroutine _frictionCoroutine;

        private bool _speedLimitationEnabled = true;
        private Coroutine _wallRunningCalculationCoroutine;

        public PlayerMovement(Rigidbody rigidbody, IPlayerMovementSettings movementSettings,
            IGroundChecker groundChecker, IWallChecker wallChecker,
            IPlayerMovementValuesCalculator movementValuesCalculator, ICoroutineStarter coroutineStarter) : base(
            rigidbody)
        {
            _groundChecker = groundChecker;
            _wallChecker = wallChecker;
            _movementSettings = movementSettings;
            _coroutineStarter = coroutineStarter;
            _cashedTransform = _rigidbody.transform;
            _originalParent = _cashedTransform.parent;
            MainRigidbody = new ReadonlyRigidbody(rigidbody);
            _movementValuesCalculator = movementValuesCalculator;

            _currentMovingState = new ValueWithReactionOnChange<MovingState>(MovingState.NotInitialized);
            _currentWallDirection = new ValueWithReactionOnChange<WallDirection>(WallDirection.Right);

            _airCoyoteTimeWaiter = new CoyoteTimeWaiter(_coroutineStarter);

            _coroutineStarter.StartCoroutine(HandleInputMovement());
            _coroutineStarter.StartCoroutine(UpdateRatioOfCurrentVelocityToMaximumVelocity());

            CurrentDashCooldownRatio = 1f;

            OnBeforeMovingStateChanged(_currentMovingState.Value);
            _currentMovingState.Value = MovingState.OnGround;
            OnAfterMovingStateChanged(_currentMovingState.Value);
        }

        public event Action<float> DashCooldownRatioChanged;
        public event Action DashAiming;
        public event Action DashAimingCanceled;
        public event Action Dashed;
        public event Action Land;
        public event Action GroundJump;
        public event Action AirJump;
        public event Action Fall;
        public event Action<WallDirection> StartWallRunning;
        public event Action<WallDirection> WallRunningDirectionChanged;
        public event Action EndWallRunning;

        private enum MovingState
        {
            NotInitialized,
            OnGround,
            CoyoteTime,
            InAir,
            WallRunning,
            DashAiming
        }

        public Vector3 CurrentPosition => _rigidbody.position;

        public float CurrentDashCooldownRatio { get; private set; }

        public Vector2 NormalizedVelocityDirectionXY { private set; get; }
        public float RatioOfCurrentVelocityToMaximumVelocity { private set; get; }
        public IReadonlyRigidbody MainRigidbody { get; }

        protected override IMovementValuesCalculator MovementValuesCalculator => _movementValuesCalculator;

        private bool IsGrounded => _groundChecker.IsColliding;
        private bool IsInContactWithWall => _wallChecker.IsColliding;

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _rigidbody.AddForce(force, mode);
        }

        public void TryJumpInputted()
        {
            if ((_currentMovingState.Value == MovingState.OnGround ||
                 _currentMovingState.Value == MovingState.CoyoteTime ||
                 _currentMovingState.Value == MovingState.WallRunning ||
                 _currentMovingState.Value == MovingState.InAir) &&
                _currentCountOfAirJumps < MaxCountOfAirJumps)
            {
                Vector3 newVelocity = _rigidbody.velocity;
                if (newVelocity.y < 0)
                {
                    newVelocity.y = 0;
                }

                _rigidbody.velocity = newVelocity;
                Vector3 jumpForce = _currentMovingState.Value == MovingState.WallRunning
                    ? _movementValuesCalculator.CalculateJumpForce(_currentWallDirection.Value)
                    : _movementValuesCalculator.CalculateJumpForce();

                _rigidbody.AddForce(jumpForce);

                if (_currentMovingState.Value == MovingState.InAir ||
                    _currentMovingState.Value == MovingState.WallRunning)
                {
                    _currentCountOfAirJumps++;
                    AirJump?.Invoke();
                }
                else if (_currentMovingState.Value == MovingState.OnGround ||
                         _currentMovingState.Value == MovingState.CoyoteTime)
                {
                    GroundJump?.Invoke();
                }
            }
        }

        public void TryStartDashAiming()
        {
            if ((_currentMovingState.Value == MovingState.InAir ||
                 _currentMovingState.Value == MovingState.WallRunning ||
                 _currentMovingState.Value == MovingState.CoyoteTime) &&
                _canDash)
            {
                _currentMovingState.Value = MovingState.DashAiming;
            }
        }

        public void TryDash(Vector3 cameraForwardDirection)
        {
            if (_currentMovingState.Value == MovingState.DashAiming)
            {
                _coroutineStarter.StartCoroutine(DashDisableSpeedLimitation());
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.AddForce(cameraForwardDirection * _movementValuesCalculator.DashForce);
                _currentMovingState.Value = IsGrounded ? MovingState.OnGround : MovingState.InAir;
            }
        }

        public void MoveInputted(Vector2 direction2d)
        {
            NormalizedVelocityDirectionXY = direction2d;
            _movementValuesCalculator.UpdateMoveInput(direction2d);
        }

        public void StickToPlatform(Transform platformTransform)
        {
            _cashedTransform.SetParent(platformTransform);
        }

        public void DisableMoving()
        {
            _rigidbody.constraints = RigidbodyConstraintsFreezeRotationAndPosition;
        }

        public void UnstickFromPlatform()
        {
            _cashedTransform.SetParent(_originalParent);
        }

        protected override void SubscribeOnEvents()
        {
            _groundChecker.ContactStateChanged += OnGroundedStatusChanged;
            _wallChecker.ContactStateChanged += OnWallContactStatusChanged;
            _airCoyoteTimeWaiter.Finished += OnAirCoyoteTimeFinished;
            _currentMovingState.BeforeValueChanged += OnBeforeMovingStateChanged;
            _currentMovingState.AfterValueChanged += OnAfterMovingStateChanged;
            _currentWallDirection.AfterValueChanged += OnWallDirectionChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _groundChecker.ContactStateChanged -= OnGroundedStatusChanged;
            _wallChecker.ContactStateChanged -= OnWallContactStatusChanged;
            _airCoyoteTimeWaiter.Finished -= OnAirCoyoteTimeFinished;
            _currentMovingState.BeforeValueChanged -= OnBeforeMovingStateChanged;
            _currentMovingState.AfterValueChanged -= OnAfterMovingStateChanged;
            _currentWallDirection.AfterValueChanged -= OnWallDirectionChanged;
        }

        private IEnumerator HandleInputMovement()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                ApplyGravity(_movementValuesCalculator.GravityForce);

                _rigidbody.AddForce(_movementValuesCalculator.MoveForce * Time.fixedDeltaTime);

                if (_speedLimitationEnabled)
                {
                    TryLimitCurrentSpeed();
                }

                yield return waitForFixedUpdate;
            }
        }

        private IEnumerator UpdateRatioOfCurrentVelocityToMaximumVelocity()
        {
            while (true)
            {
                RatioOfCurrentVelocityToMaximumVelocity =
                    _rigidbody.velocity.magnitude / _movementValuesCalculator.BaseMaximumSpeed;
                yield return null;
            }
        }

        private IEnumerator ApplyFrictionContinuously()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                _rigidbody.AddForce(_movementValuesCalculator.FrictionForce * Time.fixedDeltaTime);
                yield return waitForFixedUpdate;
            }
        }

        private IEnumerator WaitForDashCooldownWithTicking()
        {
            _canDash = false;
            float startTime = Time.time;
            float passedTime;
            do
            {
                yield return null;
                passedTime = Time.time - startTime;
                UpdateCooldownRatio(passedTime / _movementSettings.DashCooldownSeconds);
            } while (passedTime < _movementSettings.DashCooldownSeconds);

            _canDash = true;
            UpdateCooldownRatio(1f);
        }

        private IEnumerator DashDisableSpeedLimitation()
        {
            _speedLimitationEnabled = false;
            yield return new WaitForSeconds(_movementSettings.DashSpeedLimitationsDisablingForSeconds);
            _speedLimitationEnabled = true;
        }

        private IEnumerator CalculateCurrentWallDirectionContinuously()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                _currentWallDirection.Value = CalculateCurrentWallDirection();
                yield return waitForFixedUpdate;
            }
        }

        private void UpdateCooldownRatio(float newCooldownRatio)
        {
            CurrentDashCooldownRatio = newCooldownRatio;
            DashCooldownRatioChanged?.Invoke(CurrentDashCooldownRatio);
        }

        private void OnAirCoyoteTimeFinished()
        {
            if (_currentMovingState.Value == MovingState.CoyoteTime)
            {
                _currentMovingState.Value = MovingState.InAir;
            }
        }

        private void OnWallDirectionChanged(WallDirection newWallDirection)
        {
            WallRunningDirectionChanged?.Invoke(newWallDirection);
        }

        private void OnGroundedStatusChanged(bool isGrounded)
        {
            if (isGrounded)
            {
                _currentMovingState.Value = MovingState.OnGround;
            }
            else
            {
                if (IsInContactWithWall)
                {
                    _currentMovingState.Value = MovingState.WallRunning;
                }
                else
                {
                    _currentMovingState.Value = MovingState.CoyoteTime;
                }
            }
        }

        private void OnWallContactStatusChanged(bool isContactedWithWall)
        {
            if (isContactedWithWall && _currentMovingState.Value == MovingState.InAir)
            {
                _currentMovingState.Value = MovingState.WallRunning;
            }
            else if (!isContactedWithWall && _currentMovingState.Value == MovingState.WallRunning)
            {
                _currentMovingState.Value = IsGrounded ? MovingState.OnGround : MovingState.InAir;
            }
        }

        private WallDirection CalculateCurrentWallDirection()
        {
            Vector3 closestPoint = _wallChecker.Colliders[0].ClosestPoint(CurrentPosition);
            float dot = Vector3.Dot(_cashedTransform.right, closestPoint - CurrentPosition);
            return dot < 0 ? WallDirection.Left : WallDirection.Right;
        }

        private void OnBeforeMovingStateChanged(MovingState movingState)
        {
            switch (movingState)
            {
                case MovingState.OnGround:
                    if (_frictionCoroutine != null)
                    {
                        _coroutineStarter.StopCoroutine(_frictionCoroutine);
                        _frictionCoroutine = null;
                    }

                    break;
                case MovingState.CoyoteTime:
                    _airCoyoteTimeWaiter.Cancel();
                    if (_frictionCoroutine != null)
                    {
                        _coroutineStarter.StopCoroutine(_frictionCoroutine);
                        _frictionCoroutine = null;
                    }

                    break;
                case MovingState.InAir when _frictionCoroutine != null:
                    _coroutineStarter.StopCoroutine(_frictionCoroutine);
                    _frictionCoroutine = null;
                    break;
                case MovingState.WallRunning:
                    _coroutineStarter.StopCoroutine(_wallRunningCalculationCoroutine);
                    _wallRunningCalculationCoroutine = null;
                    EndWallRunning?.Invoke();
                    break;
                case MovingState.DashAiming:
                    DashAimingCanceled?.Invoke();
                    Dashed?.Invoke();
                    _coroutineStarter.StartCoroutine(WaitForDashCooldownWithTicking());
                    break;
                case MovingState.NotInitialized:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movingState), movingState, null);
            }
        }

        private void OnAfterMovingStateChanged(MovingState movingState)
        {
            switch (movingState)
            {
                case MovingState.OnGround:
                    _currentCountOfAirJumps = 0;
                    _movementValuesCalculator.ChangePlayerInputForceMultiplier(NormalPlayerInputForceMultiplier);
                    _movementValuesCalculator.ChangeGravityForceMultiplier(_movementSettings
                        .NormalGravityForceMultiplier);
                    _movementValuesCalculator.ChangeFrictionCoefficient(_movementSettings.NormalFrictionCoefficient);
                    _movementValuesCalculator.DecreaseAdditionalMaximumSpeed(_movementSettings
                        .GroundDecreaseAdditionalMaximumSpeedAcceleration);
                    _frictionCoroutine ??= _coroutineStarter.StartCoroutine(ApplyFrictionContinuously());
                    Land?.Invoke();
                    break;
                case MovingState.CoyoteTime:
                    _currentCountOfAirJumps = 0;
                    _movementValuesCalculator.ChangePlayerInputForceMultiplier(NormalPlayerInputForceMultiplier);
                    _movementValuesCalculator.ChangeGravityForceMultiplier(_movementSettings
                        .NormalGravityForceMultiplier);
                    _movementValuesCalculator.ChangeFrictionCoefficient(_movementSettings.NormalFrictionCoefficient);
                    _movementValuesCalculator.DecreaseAdditionalMaximumSpeed(_movementSettings
                        .GroundDecreaseAdditionalMaximumSpeedAcceleration);
                    _frictionCoroutine ??= _coroutineStarter.StartCoroutine(ApplyFrictionContinuously());
                    _airCoyoteTimeWaiter.Start(_movementSettings.CoyoteTimeInSeconds);
                    break;
                case MovingState.InAir:
                    _currentCountOfAirJumps = 0;
                    _movementValuesCalculator.ChangePlayerInputForceMultiplier(AirPlayerInputForceMultiplier);
                    _movementValuesCalculator.ChangeGravityForceMultiplier(_movementSettings
                        .NormalGravityForceMultiplier);
                    _movementValuesCalculator.ChangeFrictionCoefficient(_movementSettings.FlyingFrictionCoefficient);
                    _movementValuesCalculator.DecreaseAdditionalMaximumSpeed(_movementSettings
                        .AirDecreaseAdditionalMaximumSpeedAcceleration);
                    _frictionCoroutine ??= _coroutineStarter.StartCoroutine(ApplyFrictionContinuously());
                    Fall?.Invoke();
                    if (IsGrounded)
                    {
                        _currentMovingState.Value = MovingState.OnGround;
                    }

                    break;
                case MovingState.WallRunning:
                    _currentCountOfAirJumps = 0;
                    _movementValuesCalculator.ChangePlayerInputForceMultiplier(WallRunningPlayerInputForceMultiplier);
                    _movementValuesCalculator.ChangeGravityForceMultiplier(_movementSettings
                        .WallRunningGravityForceMultiplier);
                    _currentWallDirection.Value = CalculateCurrentWallDirection();
                    StartWallRunning?.Invoke(_currentWallDirection.Value);
                    _wallRunningCalculationCoroutine ??=
                        _coroutineStarter.StartCoroutine(CalculateCurrentWallDirectionContinuously());
                    _movementValuesCalculator.IncreaseAdditionalMaximumSpeed(
                        _movementSettings.WallRunningIncreaseAdditionalMaximumSpeedAcceleration,
                        _movementSettings.WallRunningIncreaseLimitAdditionalMaximumSpeedAcceleration);
                    if (IsGrounded)
                    {
                        _currentMovingState.Value = MovingState.OnGround;
                    }

                    break;
                case MovingState.DashAiming:
                    _movementValuesCalculator.ChangePlayerInputForceMultiplier(DashAimingPlayerInputForceMultiplier);
                    DashAiming?.Invoke();
                    break;
                case MovingState.NotInitialized:
                default:
                    throw new ArgumentOutOfRangeException(nameof(movingState), movingState, null);
            }
        }
    }
}