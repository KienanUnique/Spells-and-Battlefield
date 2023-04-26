using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class MovementBase : MonoBehaviour
{
    [SerializeField] protected float _moveForce = 4500f;
    [SerializeField] protected float _maximumSpeed = 15f;
    [Range(0, 1f)] [SerializeField] protected float _frictionCoefficient = 0.175f;
    protected Rigidbody _rigidbody;
    protected float _currentSpeedRatio = 1;
    private const float StopVelocityMagnitude = 0.0001f;

    protected float CurrentMaximumSpeed => _maximumSpeed * _currentSpeedRatio;

    public void MultiplySpeedRatioBy(float speedRatio)
    {
        _currentSpeedRatio *= speedRatio;
        _rigidbody.velocity *= speedRatio;
    }

    public void DivideSpeedRatioBy(float speedRatio)
    {
        _currentSpeedRatio /= speedRatio;
        _rigidbody.velocity /= speedRatio;
    }

    protected abstract void SpecialAwakeAction();

    protected void TryLimitCurrentSpeed()
    {
        if (_rigidbody.velocity.magnitude > CurrentMaximumSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * CurrentMaximumSpeed;
        }
        else if (_rigidbody.velocity.magnitude < StopVelocityMagnitude)
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        SpecialAwakeAction();
    }
}