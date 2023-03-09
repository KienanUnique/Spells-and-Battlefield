using System;
using UnityEngine;

public partial class PlayerMovement : MonoBehaviour
{
    public Transform LocalTransform { private set; get; }
    public Action<MovingStatusEnum> MovingStatusChanged;
    [SerializeField] private float _moveVelocity;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _gravityVelocity;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GroundChecker _groundChecker;
    private Vector2 _inputedMoveDirection = Vector2.zero;
    private ValueWithReactionOnChange<MovingStatusEnum> _isMoving;
    private const float StopMovingMinimumVelocity = 0.1f;

    public void Jump()
    {
        if (_groundChecker.IsGrounded)
        {
            _rigidbody.velocity += _jumpVelocity * Vector3.up;
        }
    }

    public void Move(Vector2 direction2d){
        _inputedMoveDirection = direction2d;
    }

    private void Awake()
    {
        _isMoving = new ValueWithReactionOnChange<MovingStatusEnum>();
        LocalTransform = _rigidbody.transform;
    }

    private void OnEnable()
    {
        _isMoving.ValueChanged += OnMovingStatusChanged;
    }

    private void OnDisable()
    {
        _isMoving.ValueChanged -= OnMovingStatusChanged;
    }

    private void FixedUpdate()
    {
        var localDirection = LocalTransform.TransformDirection(new Vector3(_inputedMoveDirection.x, 0, _inputedMoveDirection.y));
        var needVelocity = localDirection * _moveVelocity;
        needVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = needVelocity;

        _isMoving.Value = _rigidbody.velocity.magnitude <= StopMovingMinimumVelocity ? MovingStatusEnum.Idle : MovingStatusEnum.Running;
        _rigidbody.AddForce(_rigidbody.mass * _gravityVelocity * Vector3.down);
    }

    private void OnMovingStatusChanged(MovingStatusEnum movingStatus)
    {
        MovingStatusChanged?.Invoke(movingStatus);
    }
}
