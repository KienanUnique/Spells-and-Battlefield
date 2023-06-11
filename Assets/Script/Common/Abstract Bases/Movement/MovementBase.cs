using Settings.Sections.Movement;
using UnityEngine;

namespace Common.Abstract_Bases.Movement
{
    public abstract class MovementBase : BaseWithDisabling, IMovementBase
    {
        protected const float StopVelocityMagnitude = 0.0001f;

        protected readonly Rigidbody _rigidbody;
        protected float _currentSpeedRatio = 1;
        protected readonly MovementSettingsSection MovementSettings;

        protected MovementBase(Rigidbody rigidbody, MovementSettingsSection movementSettings)
        {
            _rigidbody = rigidbody;
            MovementSettings = movementSettings;
        }

        protected float CurrentMaximumSpeed => MovementSettings.MaximumSpeed * _currentSpeedRatio;

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

        protected virtual void TryLimitCurrentSpeed()
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

        protected void ApplyGravity(float gravityForce)
        {
            _rigidbody.AddForce(gravityForce * Vector3.down);
        }
    }
}