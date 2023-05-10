using System;
using System.Collections;
using Checkers;
using Game_Managers;
using General_Settings_in_Scriptable_Objects;
using UnityEngine;
using Zenject;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    public class PlayerMovement : MovementBase
    {
        private const int MaxCountOfAirJumps = 1;
        private const float AirPlayerInputForceMultiplier = 0.5f;
        private const float NormalPlayerInputForceMultiplier = 1;
        private const float WallRunningPlayerInputForceMultiplier = 1.5f;
        private const float DashAimingPlayerInputForceMultiplier = 0;

        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private WallChecker _wallChecker;

        private Vector2 _inputMoveDirection = Vector2.zero;
        private ValueWithReactionOnChange<MovingState> _currentMovingState;
        private int _currentCountOfAirJumps = 0;
        private Coroutine _frictionCoroutine;
        private float _currentPlayerInputForceMultiplier;
        private float _currentGravityForce;
        private bool _canDash = true;
        private bool _speedLimitationEnabled = true;
        private PlayerSettings.PlayerMovementSettingsSection _movementSettings;

        [Inject]
        private void Construct(PlayerSettings settings)
        {
            _movementSettings = settings.Movement;
        }

        public event Action Land;
        public event Action GroundJump;
        public event Action AirJump;
        public event Action Fall;
        public event Action<WallDirection> StartWallRunning;
        public event Action EndWallRunning;
        public event Action<float> DashCooldownTimerTick;
        public event Action DashCooldownFinished;
        public event Action DashAiming;
        public event Action Dashed;

        private enum MovingState
        {
            OnGround,
            InAir,
            WallRunning,
            DashAiming
        }

        public Transform MainTransform { private set; get; }
        public Vector2 NormalizedVelocityDirectionXY { private set; get; }
        public float RatioOfCurrentVelocityToMaximumVelocity { private set; get; }
        public Vector3 CurrentPosition => _rigidbody.position;

        protected override MovementSettingsSectionBase MovementBaseSettings => _movementSettings;

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
                StartCoroutine(DashDisableSpeedLimitation());
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

        protected override void SpecialAwakeAction()
        {
            MainTransform = _rigidbody.transform;
            _currentMovingState = new ValueWithReactionOnChange<MovingState>(MovingState.OnGround);
        }

        private void OnEnable()
        {
            _groundChecker.ContactStateChanged += OnGroundedStatusChanged;
            _wallChecker.ContactStateChanged += OnWallContactStatusChanged;
            _currentMovingState.BeforeValueChanged += OnBeforeMovingStateChanged;
            _currentMovingState.AfterValueChanged += OnAfterMovingStateChanged;
        }

        private void OnDisable()
        {
            _groundChecker.ContactStateChanged -= OnGroundedStatusChanged;
            _wallChecker.ContactStateChanged -= OnWallContactStatusChanged;
            _currentMovingState.BeforeValueChanged -= OnBeforeMovingStateChanged;
            _currentMovingState.AfterValueChanged -= OnAfterMovingStateChanged;
        }

        private void Start()
        {
            _currentMovingState.Value = MovingState.OnGround;
            _currentPlayerInputForceMultiplier = NormalPlayerInputForceMultiplier;
            _currentGravityForce = _movementSettings.NormalGravityForce;
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(_currentGravityForce * Vector3.down);

            _rigidbody.AddForce(_inputMoveDirection.x * _movementSettings.MoveForce * Time.deltaTime *
                                _currentPlayerInputForceMultiplier * _currentSpeedRatio * MainTransform.right);
            _rigidbody.AddForce(_inputMoveDirection.y * _movementSettings.MoveForce * Time.deltaTime *
                                _currentPlayerInputForceMultiplier * _currentPlayerInputForceMultiplier *
                                _currentSpeedRatio * MainTransform.forward);
            if (_speedLimitationEnabled)
            {
                TryLimitCurrentSpeed();
            }
        }

        private void Update()
        {
            RatioOfCurrentVelocityToMaximumVelocity =
                _rigidbody.velocity.magnitude / _movementSettings.MaximumSpeed;
        }

        private IEnumerator ApplyFrictionContinuously()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                var inverseVelocity = -_rigidbody.transform.InverseTransformDirection(_rigidbody.velocity);

                if (_inputMoveDirection.x == 0)
                {
                    _rigidbody.AddForce(inverseVelocity.x * MainTransform.right * _movementSettings.MoveForce *
                                        _movementSettings.FrictionCoefficient * Time.deltaTime);
                }

                if (_inputMoveDirection.y == 0)
                {
                    _rigidbody.AddForce(inverseVelocity.z * MainTransform.forward * _movementSettings.MoveForce *
                                        _movementSettings.FrictionCoefficient * Time.deltaTime);
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

        private void OnBeforeMovingStateChanged(MovingState movingState)
        {
            switch (movingState)
            {
                case MovingState.OnGround:
                    break;
                case MovingState.InAir when _frictionCoroutine != null:
                    StopCoroutine(_frictionCoroutine);
                    _frictionCoroutine = null;
                    break;
                case MovingState.WallRunning:
                    EndWallRunning?.Invoke();
                    break;
                case MovingState.DashAiming:
                    Dashed?.Invoke();
                    StartCoroutine(WaitForDashCooldownWithTicking());
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
                    Land?.Invoke();
                    break;
                case MovingState.InAir:
                    _currentPlayerInputForceMultiplier = AirPlayerInputForceMultiplier;
                    _currentGravityForce = _movementSettings.NormalGravityForce;
                    _frictionCoroutine ??= StartCoroutine(ApplyFrictionContinuously());
                    Fall?.Invoke();
                    break;
                case MovingState.WallRunning:
                    _currentCountOfAirJumps = 0;
                    _currentPlayerInputForceMultiplier = WallRunningPlayerInputForceMultiplier;
                    _currentGravityForce = _movementSettings.WallRunningGravityForce;
                    var closestPoint = _wallChecker.Colliders[0].ClosestPoint(CurrentPosition);
                    var dot = Vector3.Dot(MainTransform.right, closestPoint - CurrentPosition);
                    StartWallRunning?.Invoke(dot < 0 ? WallDirection.Left : WallDirection.Right);
                    break;
                case MovingState.DashAiming:
                    _currentPlayerInputForceMultiplier = DashAimingPlayerInputForceMultiplier;
                    DashAiming?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movingState), movingState, null);
            }
        }
    }
}