using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using UnityEngine;

namespace Enemies.Look_Point_Calculator
{
    public interface ILookPointCalculator
    {
        public void SetLookData(IReadonlyRigidbody thisRigidbody, IReadonlyTransform thisPositionReferencePoint,
            IEnemyTarget target);

        public void UpdateTarget(IEnemyTarget target);
        public Vector3 CalculateLookPointDirection();
        public void ChangeThisPositionReferencePointTransform(IReadonlyTransform newReferenceTransform);
    }
}