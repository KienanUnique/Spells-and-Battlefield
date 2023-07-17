using Common.Readonly_Rigidbody;
using Interfaces;
using UnityEngine;

namespace Enemies.Look_Point_Calculator
{
    public interface ILookPointCalculator
    {
        public void SetLookData(IReadonlyRigidbody thisRigidbody, IEnemyTarget target);
        public Quaternion CalculateLookPointDirection();
    }
}