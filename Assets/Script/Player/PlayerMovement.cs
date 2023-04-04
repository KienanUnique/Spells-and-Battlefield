using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public Action LandEvent;
        public Action JumpEvent;
        public Action FallEvent;
        public Transform LocalTransform { private set; get; }
        public Vector2 NormalizedVelocityDirectionXY { private set; get; }
        public float RatioOfCurrentVelocityToMaximumVelocity { private set; get; }
        public Vector3 CurrentPosition => _rigidbody.position;

        [SerializeField] private float _runVelocity = 10f;
        [SerializeField] private float _walkVelocityMagnitude = 4;
        [SerializeField] private float _jumpVelocity = 13;
        [SerializeField] private float _gravityVelocity = 15;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GroundChecker _groundChecker;
        private Vector2 _inputMoveDirection = Vector2.zero;
        private InputMovingEnum _inputMovingTypeRequired;
        private ValueWithReactionOnChange<bool> _isGrounded;

        public void Jump()
        {
            if (_isGrounded.Value)
            {
                _rigidbody.velocity += _jumpVelocity * Vector3.up;
                JumpEvent?.Invoke();
            }
        }

        public void Move(Vector2 direction2d)
        {
            NormalizedVelocityDirectionXY = direction2d;
            _inputMoveDirection = direction2d;
        }

        public void StartWalking()
        {
            _inputMovingTypeRequired = InputMovingEnum.Walk;
        }

        public void StartRunning()
        {
            _inputMovingTypeRequired = InputMovingEnum.Run;
        }

        private void Awake()
        {
            LocalTransform = _rigidbody.transform;
            _inputMovingTypeRequired = InputMovingEnum.Run;
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

        private void OnGroundedStatusChanged(bool isGrounded)
        {
            if (isGrounded)
            {
                LandEvent?.Invoke();
            }
            else
            {
                FallEvent?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            _isGrounded.Value = _groundChecker.IsGrounded;

            var localDirection =
                LocalTransform.TransformDirection(new Vector3(_inputMoveDirection.x, 0, _inputMoveDirection.y));
            var currentVelocityMagnitude = _inputMovingTypeRequired switch
            {
                InputMovingEnum.Walk => _walkVelocityMagnitude,
                InputMovingEnum.Run => _runVelocity,
                _ => 0
            };

            var currentVelocity = _rigidbody.velocity;
            var needVelocity = localDirection * currentVelocityMagnitude -
                               (new Vector3(currentVelocity.x, 0, currentVelocity.z));
            _rigidbody.AddForce(needVelocity, ForceMode.VelocityChange);

            _rigidbody.AddForce(_rigidbody.mass * _gravityVelocity * Vector3.down);
        }

        private void Update()
        {
            RatioOfCurrentVelocityToMaximumVelocity = _rigidbody.velocity.magnitude / _runVelocity;
        }

        private enum InputMovingEnum
        {
            Walk,
            Run
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _rigidbody.AddForce(force, mode);
        }
    }
}