using System;
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
        private Vector2 _inputMoveDirection = Vector2.zero;
        private ValueWithReactionOnChange<bool> _isGrounded;
        private MovingState _currentMovingState;
        private const int MaxCountOfAirJumps = 1;
        private int _currentCountOfAirJumps = 0;

        public void TryJump()
        {
            if (_currentMovingState == MovingState.OnGround || (_currentMovingState == MovingState.InAir &&
                                                                _currentCountOfAirJumps < MaxCountOfAirJumps))
            {
                _rigidbody.AddForce(_jumpForce * Vector3.up);
                switch (_currentMovingState)
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
            _isGrounded = new ValueWithReactionOnChange<bool>(true);
        }

        private void OnEnable()
        {
            _isGrounded.ValueChanged += OnGroundedStatusChanged;
        }

        private void OnDisable()
        {
            _isGrounded.ValueChanged -= OnGroundedStatusChanged;
        }

        private void Start()
        {
            _currentMovingState = MovingState.OnGround;
        }

        private void OnGroundedStatusChanged(bool isGrounded)
        {
            if (isGrounded)
            {
                _currentCountOfAirJumps = 0;
                _currentMovingState = MovingState.OnGround;
                LandEvent?.Invoke();
            }
            else
            {
                _currentMovingState = MovingState.InAir;
                FallEvent?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            _isGrounded.Value = _groundChecker.IsGrounded;
            _rigidbody.AddForce(_gravityForce * Vector3.down);

            ApplyFriction();

            var multiplier = _currentMovingState == MovingState.InAir ? 0.5f : 1f;
            _rigidbody.AddForce(_inputMoveDirection.x * _runForce * Time.deltaTime * multiplier *
                                _rigidbody.transform.right);
            _rigidbody.AddForce(_inputMoveDirection.y * _runForce * Time.deltaTime * multiplier * multiplier *
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

        private void ApplyFriction()
        {
            if (_currentMovingState == MovingState.InAir) return;

            Vector3 inverseVelocity = -_rigidbody.transform.InverseTransformDirection(_rigidbody.velocity);

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
        }


        private enum MovingState
        {
            OnGround,
            InAir
        }
    }
}