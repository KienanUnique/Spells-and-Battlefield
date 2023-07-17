using Common.Readonly_Rigidbody;
using Interfaces;
using UnityEngine;

namespace Enemies.Look_Point_Calculator
{
    public abstract class LookPointCalculatorBase : ILookPointCalculator
    {
        protected IReadonlyRigidbody _thisRigidbody;
        protected IEnemyTarget _target;
        protected bool _isTargetNull;
        protected IReadonlyRigidbody TargetRigidbody => _target.MainRigidbody;

        public void SetLookData(IReadonlyRigidbody thisRigidbody, IEnemyTarget target)
        {
            _thisRigidbody = thisRigidbody;
            _target = target;
            _isTargetNull = _target == null;
        }

        public abstract Quaternion CalculateLookPointDirection();
    }
}