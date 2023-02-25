using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveVelocity;
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _gravityVelocity;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GroundChecker _groundChecker;
    private Vector2 _inputedMoveDirection = Vector2.zero;
    private Vector3 _currentJumpVelocity = Vector3.zero;

    public void FixedUpdate()
    {
        var localDirection = _rigidbody.transform.TransformDirection(new Vector3(_inputedMoveDirection.x, 0, _inputedMoveDirection.y));
        var needVelocity = localDirection * _moveVelocity;
        needVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = needVelocity;

        _rigidbody.AddForce(Vector3.down * _rigidbody.mass * _gravityVelocity);
    }

    public void Jump()
    {
        if (_groundChecker.IsGrounded)
        {
            _currentJumpVelocity = Vector3.zero;
            _rigidbody.velocity += _jumpVelocity * Vector3.up;
        }
    }

    public void Move(Vector2 direction2d) => _inputedMoveDirection = direction2d;
}
