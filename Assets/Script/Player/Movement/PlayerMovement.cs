using System;
using System.Collections;
using Common;
using Common.Abstract_Bases.Checkers;
using Common.Abstract_Bases.Movement;
using Common.Readonly_Transform;
using Interfaces;
using Settings;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Player.Movement
{
    public sealed class PlayerMovement : MovementBase, IPlayerMovement
    {
        private const int MaxCountOfAirJumps = 1;
        private const float AirPlayerInputForceMultiplier = 0.5f;
        private const float NormalPlayerInputForceMultiplier = 1;
        private const float WallRunningPlayerInputForceMultiplier = 1.5f;
        private const float DashAimingPlayerInputForceMultiplier = 0;

        private readonly Transform _cashedTransform;
        private readonly GroundChecker _groundChecker;
        private readonly WallChecker _wallChecker;
        private readonly PlayerSettings.PlayerMovementSettingsSection _movementSettings;
        private readonly ICoroutineStarter _coroutineStarter;

        private Vector2 _inputMoveDirection = Vector2.zero;
        private readonly ValueWithReactionOnChange<MovingState> _currentMovingState;
        private readonly ValueWithReactionOnChange<WallDirection> _currentWallDirection;
        private int _currentCountOfAirJumps;
        private Coroutine _frictionCoroutine;
        private Coroutine _wallRunningCalculationCoroutine;
        private float _currentPlayerInputForceMultiplier;
        private float _currentGravityForce;
        private bool _canDash = true;
        private bool _speedLimitationEnabled = true;

        public PlayerMovement(Rigidbody rigidbody, PlayerSettings.PlayerMovementSettingsSection movementSettings,
            GroundChecker groundChecker, WallChecker wallChecker, ICoroutineStarter coroutineStarter) :
            base(rigidbody, movementSettings)
        {
            _groundChecker = groundChecker;
            _wallChecker = wallChecker;
            _movementSettings = movementSettings;
            _coroutineStarter = coroutineStarter;
            _cashedTransform = _rigidbody.transform;
            MainTransform = new ReadonlyTransform(_cashedTransform);

            _currentMovingState = new ValueWithReactionOnChange<MovingState>(MovingState.NotInitialized);
            _currentWallDirection = new ValueWithReactionOnChange<WallDirection>(WallDirection.Right);

            _coroutineStarter.StartCoroutine(HandleInputMovement());
            _coroutineStarter.StartCoroutine(UpdateRatioOfCurrentVelocityToMaximumVelocity());

            SubscribeOnEvents();

            _currentMovingState.Value = MovingState.OnGround;
        }

        public event Action Land;
        public event Action GroundJump;
        public event Action AirJump;
        public event Action Fall;
        public event Action<WallDirection> StartWallRunning;
        public event Action<WallDirection> WallRunningDirectionChanged;
        public event Action EndWallRunning;
        public event Action<float> DashCooldownTimerTick;
        public event Action DashCooldownFinished;
        public event Action DashAiming;
        public event Action Dashed;

        private enum MovingState
        {
            NotInitialized,
            OnGround,
            InAir,
            WallRunning,
            DashAiming
        }

        public Vector2 NormalizedVelocityDirectionXY { private set; get; }
        public float RatioOfCurrentVelocityToMaximumVelocity { private set; get; }
        public IReadonlyTransform MainTransform { get; }
        public Vector3 CurrentPosition => _rigidbody.position;

        private bool IsGrounded => _groundChecker.IsColliding;
        private bool IsInContactWithWall => _wallChecker.IsColliding;

        public void TryJumpInputted()
        {
            if (_currentMovingState.Value == MovingState.OnGround ||
                _currentMovingState.Value == MovingState.WallRunning ||
                (_currentMovingState.Value == MovingState.InAir && _currentCountOfAirJumps < MaxCountOfAirJumps))
            {
                _rigidbody.AddForce(_movementSettings.JumpForce * Vector3.up);
                if (_currentMovingState.Value == MovingState.InAir ||
                    _currentMovingState.Value == MovingState.WallRunning)
                {
                    _currentCountOfAirJumps++;
                    AirJump?.Invoke();
                }
                else if (_currentMovingState.Value == MovingState.OnGround)
                {
                    GroundJump?.Invoke();
                }
            }
        }

        public void TryStartDashAiming()
        {
            if ((_currentMovingState.Value == MovingState.InAir ||
                 _currentMovingState.Value == MovingState.WallRunning) && _canDash)
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
                _rigidbody.AddForce(cameraForwardDirection * _movementSettings.DashForce);
                _currentMovingState.Value = IsGrounded ? MovingState.OnGround : MovingState.InAir;
            }
        }

        public void MoveInputted(Vector2 direction2d)
        {
            NormalizedVelocityDirectionXY = direction2d;
            _inputMoveDirection = direction2d;
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _rigidbody.AddForce(force, mode);
        }

        protected override void SubscribeOnEvents()
        {
            _groundChecker.ContactStateChanged += OnGroundedStatusChanged;
            _wallChecker.ContactStateChanged += OnWallContactStatusChanged;
            _currentMovingState.BeforeValueChanged += OnBeforeMovingStateChanged;
            _currentMovingState.AfterValueChanged += OnAfterMovingStateChanged;
            _currentWallDirection.AfterValueChanged += OnWallDirectionChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            _groundChecker.ContactStateChanged -= OnGroundedStatusChanged;
            _wallChecker.ContactStateChanged -= OnWallContactStatusChanged;
            _currentMovingState.BeforeValueChanged -= OnBeforeMovingStateChanged;
            _currentMovingState.AfterValueChanged -= OnAfterMovingStateChanged;
            _currentWallDirection.AfterValueChanged -= OnWallDirectionChanged;
        }

        private IEnumerator HandleInputMovement()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                ApplyGravity(_currentGravityForce);

                _rigidbody.AddForce(_inputMoveDirection.x * _movementSettings.MoveForce * Time.deltaTime *
                                    _currentPlayerInputForceMultiplier * _currentSpeedRatio * _cashedTransform.right);
                _rigidbody.AddForce(_inputMoveDirection.y * _movementSettings.MoveForce * Time.deltaTime *
                                    _currentPlayerInputForceMultiplier * _currentPlayerInputForceMultiplier *
                                    _currentSpeedRatio * _cashedTransform.forward);
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
                    _rigidbody.velocity.magnitude / _movementSettings.MaximumSpeed;
                yield return null;
            }
        }

        private IEnumerator ApplyFrictionContinuously(float frictionCoefficient)
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                var inverseVelocity = -_rigidbody.transform.InverseTransformDirection(_rigidbody.velocity);

                if (_inputMoveDirection.x == 0)
                {
                    _rigidbody.AddForce(inverseVelocity.x * _movementSettings.MoveForce * _cashedTransform.right *
                                        frictionCoefficient * Time.deltaTime);
                }

                if (_inputMoveDirection.y == 0)
                {
                    _rigidbody.AddForce(inverseVelocity.z * _movementSettings.MoveForce * _cashedTransform.forward *
                                        frictionCoefficient * Time.deltaTime);
                }

                yield return waitForFixedUpdate;
            }
        }

        private IEnumerator WaitForDashCooldownWithTicking()
        {
            _canDash = false;
            var startTime = Time.time;
            float passedTime;
            do
            {
                yield return null;
                passedTime = Time.time - startTime;
                DashCooldownTimerTick?.Invoke(passedTime / _movementSettings.DashCooldownSeconds);
            } while (passedTime < _movementSettings.DashCooldownSeconds);

            _canDash = true;
            DashCooldownFinished?.Invoke();
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
                _currentMovingState.Value = IsInContactWithWall ? MovingState.WallRunning : MovingState.InAir;
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
            var closestPoint = _wallChecker.Colliders[0].ClosestPoint(CurrentPosition);
            var dot = Vector3.Dot(_cashedTransform.right, closestPoint - CurrentPosition);
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
                    _currentPlayerInputForceMultiplier = NormalPlayerInputForceMultiplier;
                    _currentGravityForce = _movementSettings.NormalGravityForce;
                    _frictionCoroutine ??=
                        _coroutineStarter.StartCoroutine(
                            ApplyFrictionContinuously(_movementSettings.NormalFrictionCoefficient));
                    Land?.Invoke();
                    break;
                case MovingState.InAir:
                    _currentCountOfAirJumps = 0;
                    _currentPlayerInputForceMultiplier = AirPlayerInputForceMultiplier;
                    _currentGravityForce = _movementSettings.NormalGravityForce;
                    _frictionCoroutine ??=
                        _coroutineStarter.StartCoroutine(
                            ApplyFrictionContinuously(_movementSettings.FlyingFrictionCoefficient));
                    Fall?.Invoke();
                    break;
                case MovingState.WallRunning:
                    _currentCountOfAirJumps = 0;
                    _currentPlayerInputForceMultiplier = WallRunningPlayerInputForceMultiplier;
                    _currentGravityForce = _movementSettings.WallRunningGravityForce;
                    _currentWallDirection.Value = CalculateCurrentWallDirection();
                    StartWallRunning?.Invoke(_currentWallDirection.Value);
                    _wallRunningCalculationCoroutine ??=
                        _coroutineStarter.StartCoroutine(CalculateCurrentWallDirectionContinuously());
                    break;
                case MovingState.DashAiming:
                    _currentPlayerInputForceMultiplier = DashAimingPlayerInputForceMultiplier;
                    DashAiming?.Invoke();
                    break;
                case MovingState.NotInitialized:
                default:
                    throw new ArgumentOutOfRangeException(nameof(movingState), movingState, null);
            }
        }
    }
}