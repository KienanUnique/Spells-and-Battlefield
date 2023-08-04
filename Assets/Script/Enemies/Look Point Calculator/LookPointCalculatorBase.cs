using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Interfaces;
using UnityEngine;

namespace Enemies.Look_Point_Calculator
{
    public abstract class LookPointCalculatorBase : ILookPointCalculator
    {
        protected IReadonlyRigidbody _thisRigidbody;
        protected IReadonlyTransform _thisPositionReferencePoint;
        protected IEnemyTarget _target;
        protected bool _isTargetNull;
        protected IReadonlyRigidbody TargetRigidbody => _target.MainRigidbody;
        protected Vector3 CurrentPosition => _thisPositionReferencePoint.Position;
        protected Vector3 CurrentVelocity => _thisRigidbody.Velocity;
        protected Vector3 DefaultRotation => _thisRigidbody.Forward;

        public abstract Vector3 CalculateLookPointDirection();

        public void SetLookData(IReadonlyRigidbody thisRigidbody, IReadonlyTransform thisPositionReferencePoint,
            IEnemyTarget target)
        {
            _thisRigidbody = thisRigidbody;
            UpdateTarget(target);
            ChangeThisPositionReferencePointTransform(thisPositionReferencePoint);
        }

        public void UpdateTarget(IEnemyTarget target)
        {
            _target = target;
            _isTargetNull = _target == null;
        }

        public void ChangeThisPositionReferencePointTransform(IReadonlyTransform newReferenceTransform)
        {
            _thisPositionReferencePoint = newReferenceTransform;
        }
    }
}