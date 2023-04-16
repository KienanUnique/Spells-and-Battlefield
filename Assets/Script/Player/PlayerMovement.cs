using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public Action LandEvent;
        public Action GroundJumpEvent;
        public Action AirJumpEvent;
        public Action FallEvent;
        public Transform LocalTransform { private set; get; }
        public Vector2 NormalizedVelocityDirectionXY { private set; get; }
        public float RatioOfCurrentVelocityToMaximumVelocity { private set; get; }
        public Vector3 CurrentPosition => _rigidbody.position;

        [SerializeField] private float _runForce = 4500f;
        [SerializeField] private float _jumpForce = 800f;
        [SerializeField] private float _gravityForce = 15;
        [Range(0, 1f)] [SerializeField] private float _groundFriction = 0.175f;
        [SerializeField] private float _maximumSpeed = 15f;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GroundChecker _groundChecker;
        private const int MaxCountOfAirJumps = 1;
        private const float AirPlayerInputForceMultiplier = 0.5f;
        private const float GroundPlayerInputForceMultiplier = 1;
        private Vector2 _inputMoveDirection = Vector2.zero;
        private ValueWithReactionOnChange<MovingState> _currentMovingState;
        private int _currentCountOfAirJumps = 0;
        private Coroutine _frictionCoroutine;
        private float _currentPlayerInputForceMultiplier;

        public void TryJump()
        {
            if (_currentMovingState.Value == MovingState.OnGround || (_currentMovingState.Value == MovingState.InAir &&
                                                                      _currentCountOfAirJumps < MaxCountOfAirJumps))
            {
                _rigidbody.AddForce(_jumpForce * Vector3.up);
                switch (_currentMovingState.Value)
                {
                    case MovingState.InAir:
                        _currentCountOfAirJumps++;
                        AirJumpEvent?.Invoke();
                        break;
                    case MovingState.OnGround:
                        GroundJumpEvent?.Invoke();
                        break;
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
            LocalTransform = _rigidbody.transform;
            _currentMovingState = new ValueWithReactionOnChange<MovingState>(MovingState.OnGround);
        }

        private void OnEnable()
        {
            _groundChecker.GroundStateChanged += OnGroundedStatusChanged;
            _currentMovingState.BeforeValueChanged += OnBeforeMovingStateChanged;
            _currentMovingState.AfterValueChanged += OnAfterMovingStateChanged;
        }

        private void OnDisable()
        {
            _groundChecker.GroundStateChanged -= OnGroundedStatusChanged;
            _currentMovingState.BeforeValueChanged -= OnBeforeMovingStateChanged;
            _currentMovingState.AfterValueChanged -= OnAfterMovingStateChanged;
        }

        private void Start()
        {
            _currentMovingState.Value = MovingState.OnGround;
            _currentPlayerInputForceMultiplier = GroundPlayerInputForceMultiplier;
        }

        private void OnGroundedStatusChanged(bool isGrounded)
        {
            if (isGrounded)
            {
                _currentCountOfAirJumps = 0;
                _currentMovingState.Value = MovingState.OnGround;
                LandEvent?.Invoke();
            }
            else
            {
                _currentMovingState.Value = MovingState.InAir;
                FallEvent?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(_gravityForce * Vector3.down);

            _rigidbody.AddForce(_inputMoveDirection.x * _runForce * Time.deltaTime *
                                _currentPlayerInputForceMultiplier * _rigidbody.transform.right);
            _rigidbody.AddForce(_inputMoveDirection.y * _runForce * Time.deltaTime *
                                _currentPlayerInputForceMultiplier * _currentPlayerInputForceMultiplier *
                                _rigidbody.transform.forward);
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
                    _rigidbody.AddForce(inverseVelocity.x * _rigidbody.transform.right * _runForce * _groundFriction *
                                        Time.deltaTime);
                }

                if (_inputMoveDirection.y == 0)
                {
                    _rigidbody.AddForce(inverseVelocity.z * _rigidbody.transform.forward * _runForce * _groundFriction *
                                        Time.deltaTime);
                }

                yield return waitForFixedUpdate;
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(movingState), movingState, null);
            }
        }

        private void OnAfterMovingStateChanged(MovingState movingState)
        {
            switch (movingState)
            {
                case MovingState.OnGround:
                    _currentPlayerInputForceMultiplier = GroundPlayerInputForceMultiplier;
                    break;
                case MovingState.InAir:
                    _currentPlayerInputForceMultiplier = AirPlayerInputForceMultiplier;
                    _frictionCoroutine ??= StartCoroutine(ApplyFriction());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movingState), movingState, null);
            }
        }

        private enum MovingState
        {
            OnGround,
            InAir
        }
    }
}