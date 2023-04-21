using System;
using System.Collections;
using Checkers;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private const int MaxCountOfAirJumps = 1;
        private const float AirPlayerInputForceMultiplier = 0.5f;
        private const float NormalPlayerInputForceMultiplier = 1;
        private const float WallRunningPlayerInputForceMultiplier = 1.5f;

        [SerializeField] private float _runForce = 4500f;
        [SerializeField] private float _jumpForce = 800f;
        [SerializeField] private float _normalGravityForce = 15;
        [SerializeField] private float _wallRunningGravityForce = 2;
        [Range(0, 1f)] [SerializeField] private float _groundFriction = 0.175f;
        [SerializeField] private float _maximumSpeed = 15f;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private WallChecker _wallChecker;

        private Vector2 _inputMoveDirection = Vector2.zero;
        private ValueWithReactionOnChange<MovingState> _currentMovingState;
        private int _currentCountOfAirJumps = 0;
        private Coroutine _frictionCoroutine;
        private float _currentPlayerInputForceMultiplier;
        private float _currentGravityForce;

        public event Action LandEvent;
        public event Action GroundJumpEvent;
        public event Action AirJumpEvent;
        public event Action FallEvent;
        public event Action<WallDirection> StartWallRunningEvent;
        public event Action EndWallRunningEvent;

        private enum MovingState
        {
            OnGround,
            InAir,
            WallRunning
        }

        public Transform MainTransform { private set; get; }
        public Vector2 NormalizedVelocityDirectionXY { private set; get; }
        public float RatioOfCurrentVelocityToMaximumVelocity { private set; get; }
        public Vector3 CurrentPosition => _rigidbody.position;
        private bool IsGrounded => _groundChecker.IsColliding;
        private bool IsInContactWithWall => _wallChecker.IsColliding;

        public void TryJump()
        {
            if (_currentMovingState.Value == MovingState.OnGround ||
                _currentMovingState.Value == MovingState.WallRunning ||
                (_currentMovingState.Value == MovingState.InAir && _currentCountOfAirJumps < MaxCountOfAirJumps))
            {
                _rigidbody.AddForce(_jumpForce * Vector3.up);
                if (_currentMovingState.Value == MovingState.InAir ||
                    _currentMovingState.Value == MovingState.WallRunning)
                {
                    _currentCountOfAirJumps++;
                    AirJumpEvent?.Invoke();
                }
                else if (_currentMovingState.Value == MovingState.OnGround)
                {
                    GroundJumpEvent?.Invoke();
                }
            }
        }

        public void Move(Vector2 direction2d)
        {
            NormalizedVelocityDirectionXY = direction2d;
            _inputMoveDirection = direction2d;
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _rigidbody.AddForce(force, mode);
        }

        private void Awake()
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
            _currentGravityForce = _normalGravityForce;
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(_currentGravityForce * Vector3.down);

            _rigidbody.AddForce(_inputMoveDirection.x * _runForce * Time.deltaTime *
                                _currentPlayerInputForceMultiplier * MainTransform.right);
            _rigidbody.AddForce(_inputMoveDirection.y * _runForce * Time.deltaTime *
                                _currentPlayerInputForceMultiplier * _currentPlayerInputForceMultiplier *
                                MainTransform.forward);
            if (_rigidbody.velocity.magnitude > _maximumSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _maximumSpeed;
            }
        }

        private void Update()
        {
            RatioOfCurrentVelocityToMaximumVelocity = _rigidbody.velocity.magnitude / _maximumSpeed;
        }

        private IEnumerator ApplyFriction()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                var inverseVelocity = -_rigidbody.transform.InverseTransformDirection(_rigidbody.velocity);

                if (_inputMoveDirection.x == 0)
                {
                    _rigidbody.AddForce(inverseVelocity.x * MainTransform.right * _runForce * _groundFriction *
                                        Time.deltaTime);
                }

                if (_inputMoveDirection.y == 0)
                {
                    _rigidbody.AddForce(inverseVelocity.z * MainTransform.forward * _runForce * _groundFriction *
                                        Time.deltaTime);
                }

                yield return waitForFixedUpdate;
            }
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
                    EndWallRunningEvent?.Invoke();
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
                    _currentGravityForce = _normalGravityForce;
                    LandEvent?.Invoke();
                    break;
                case MovingState.InAir:
                    _currentPlayerInputForceMultiplier = AirPlayerInputForceMultiplier;
                    _currentGravityForce = _normalGravityForce;
                    _frictionCoroutine ??= StartCoroutine(ApplyFriction());
                    FallEvent?.Invoke();
                    break;
                case MovingState.WallRunning:
                    _currentCountOfAirJumps = 0;
                    _currentPlayerInputForceMultiplier = WallRunningPlayerInputForceMultiplier;
                    _currentGravityForce = _wallRunningGravityForce;
                    var closestPoint = _wallChecker.Colliders[0].ClosestPoint(CurrentPosition);
                    var dot = Vector3.Dot(MainTransform.right, closestPoint - CurrentPosition);
                    StartWallRunningEvent?.Invoke(dot < 0 ? WallDirection.Left : WallDirection.Right);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movingState), movingState, null);
            }
        }
    }
}