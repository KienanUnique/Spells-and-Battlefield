using System;
using UnityEngine;

public partial class PlayerMovement : MonoBehaviour
{
    public Action LandEvent;
    public Action JumpEvent;
    public Action FallEvent;
    public Transform LocalTransform { private set; get; }
    public Vector2 NormalizedVelocityDirectionXY { private set; get; }
    public float RatioOfCurrentVelocityToMaximumVelocity { private set; get; }
    [SerializeField] private float _runVelocity;
    [SerializeField] private float _walkVelocityMagnitude;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _gravityVelocity;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GroundChecker _groundChecker;
    private Vector2 _inputedMoveDirection = Vector2.zero;
    private InputMovingEnum _inputMovingTypeRequiered;
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
        _inputedMoveDirection = direction2d;
    }

    public void StartWalking()
    {
        _inputMovingTypeRequiered = InputMovingEnum.Walk;
    }

    public void StartRunning()
    {
        _inputMovingTypeRequiered = InputMovingEnum.Run;
    }

    private void Awake()
    {
        LocalTransform = _rigidbody.transform;
        _inputMovingTypeRequiered = InputMovingEnum.Run;
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

        var localDirection = LocalTransform.TransformDirection(new Vector3(_inputedMoveDirection.x, 0, _inputedMoveDirection.y));
        float currentVelocityMagnitude = 0;
        switch (_inputMovingTypeRequiered)
        {
            case InputMovingEnum.Walk:
                currentVelocityMagnitude = _walkVelocityMagnitude;
                break;
            case InputMovingEnum.Run:
                currentVelocityMagnitude = _runVelocity;
                break;
        }
        var needVelocity = localDirection * currentVelocityMagnitude - (new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z));
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
}
