using System;
using UnityEngine;

[Serializable]
public class PlayerMovement : IModelMonoBehaviour
{
    [SerializeField] private float _moveVelocity;
    [SerializeField] private float _moveVelocitySmoothTime;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GroundChecker _groundChecker;
    private Vector3 _currentMoveVelocity = Vector3.zero;
    private Vector2 _inputedMoveDirection = Vector2.zero;

    public void FixedUpdate()
    {
        var localDirection = _rigidbody.transform.TransformDirection(new Vector3(_inputedMoveDirection.x, 0, _inputedMoveDirection.y));
        var needVelocity = localDirection * _moveVelocity;
        needVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = needVelocity;
    }

    public void Jump()
    {
        if(_groundChecker.IsGrounded){
            _rigidbody.velocity += _jumpVelocity * Vector3.up;
        }
    }

    public void Move(Vector2 direction2d) => _inputedMoveDirection = direction2d;
}
