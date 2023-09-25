using Common.Abstract_Bases.Disableable;
using Common.Abstract_Bases.Movement.Coefficients_Calculator;
using UnityEngine;

namespace Common.Abstract_Bases.Movement
{
    public abstract class MovementBase : BaseWithDisabling, IMovementBase
    {
        protected const float StopVelocityMagnitude = 0.0001f;

        protected readonly Rigidbody _rigidbody;

        protected MovementBase(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        protected abstract IMovementValuesCalculator MovementValuesCalculator { get; }

        public void MultiplySpeedRatioBy(float speedRatio)
        {
            MovementValuesCalculator.MultiplySpeedRatioBy(speedRatio);
            _rigidbody.velocity *= speedRatio;
        }

        public void DivideSpeedRatioBy(float speedRatio)
        {
            MovementValuesCalculator.DivideSpeedRatioBy(speedRatio);
            _rigidbody.velocity /= speedRatio;
        }

        protected virtual void TryLimitCurrentSpeed()
        {
            if (_rigidbody.velocity.magnitude > MovementValuesCalculator.MaximumSpeedCalculated)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized *
                                      MovementValuesCalculator.MaximumSpeedCalculated;
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