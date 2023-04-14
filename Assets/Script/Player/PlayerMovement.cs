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

        [SerializeField] private float _runVelocity = 10f;
        [SerializeField] private float _jumpVelocity = 13;
        [SerializeField] private float _gravityVelocity = 15;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GroundChecker _groundChecker;
        private Vector3 _localInputDirection;
        private Vector2 _inputMoveDirection = Vector2.zero;
        private ValueWithReactionOnChange<bool> _isGrounded;
        private float _currentVelocityMagnitude;
        private MovingState _currentMovingState;
        private const int MaxCountOfAirJumps = 1;
        private int _currentCountOfAirJumps = 0;

        public void TryJump()
        {
            if (_currentMovingState == MovingState.OnGround || (_currentMovingState == MovingState.InAir &&
                                                                _currentCountOfAirJumps < MaxCountOfAirJumps))
            {
                var needVelocity = _rigidbody.velocity;
                needVelocity.y = _jumpVelocity;
                _rigidbody.AddForce(needVelocity, ForceMode.VelocityChange); 
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
            _currentVelocityMagnitude = _runVelocity;
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

            var currentVelocity = _rigidbody.velocity;
            var needVelocity = _localInputDirection * _currentVelocityMagnitude -
                               (new Vector3(currentVelocity.x, 0, currentVelocity.z));
            _rigidbody.AddForce(needVelocity, ForceMode.VelocityChange);

            _rigidbody.AddForce(_rigidbody.mass * _gravityVelocity * Vector3.down);
        }

        private void Update()
        {
            RatioOfCurrentVelocityToMaximumVelocity = _rigidbody.velocity.magnitude / _runVelocity;
            _localInputDirection =
                LocalTransform.TransformDirection(new Vector3(_inputMoveDirection.x, 0, _inputMoveDirection.y));
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _rigidbody.AddForce(force, mode);
        }

        private enum MovingState
        {
            OnGround,
            InAir
        }
    }
}