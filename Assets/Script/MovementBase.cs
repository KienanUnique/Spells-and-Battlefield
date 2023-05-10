using General_Settings_in_Scriptable_Objects;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class MovementBase : MonoBehaviour
{
    private const float StopVelocityMagnitude = 0.0001f;

    protected Rigidbody _rigidbody;
    protected float _currentSpeedRatio = 1;
    protected abstract MovementSettingsSectionBase MovementBaseSettings { get; }
    private float CurrentMaximumSpeed => MovementBaseSettings.MaximumSpeed * _currentSpeedRatio;

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