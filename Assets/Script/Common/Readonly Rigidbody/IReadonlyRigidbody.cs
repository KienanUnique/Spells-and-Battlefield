using Common.Readonly_Transform;
using UnityEngine;

namespace Common.Readonly_Rigidbody
{
    public interface IReadonlyRigidbody : IReadonlyTransform
    {
        public float AngularDrag { get; }
        public Vector3 AngularVelocity { get; }
        public Vector3 CenterOfMass { get; }
        public CollisionDetectionMode CollisionDetectionMode { get; }
        public RigidbodyConstraints Constraints { get; }
        public bool DetectCollisions { get; }
        public float Drag { get; }
        public bool FreezeRotation { get; }
        public Vector3 InertiaTensor { get; }
        public Quaternion InertiaTensorRotation { get; }
        public RigidbodyInterpolation Interpolation { get; }
        public bool IsKinematic { get; }
        public float Mass { get; }
        public float MaxAngularVelocity { get; }
        public float MaxDepenetrationVelocity { get; }
        public float SleepThreshold { get; }
        public float SolverIterations { get; }
        public float SolverVelocityIterations { get; }
        public bool UseGravity { get; }
        public Vector3 Velocity { get; }
        public Vector3 WorldCenterOfMass { get; }
    }
}