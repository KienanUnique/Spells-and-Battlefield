using Common.Readonly_Transform;
using UnityEngine;

namespace Common.Readonly_Rigidbody
{
    public class ReadonlyRigidbody : ReadonlyTransform, IReadonlyRigidbody
    {
        private readonly Rigidbody _rigidbody;

        public ReadonlyRigidbody(Rigidbody rigidbody) : base(rigidbody.transform)
        {
            _rigidbody = rigidbody;
        }

        public float AngularDrag => _rigidbody.angularDrag;
        public Vector3 AngularVelocity => _rigidbody.angularVelocity;
        public Vector3 CenterOfMass => _rigidbody.centerOfMass;
        public CollisionDetectionMode CollisionDetectionMode => _rigidbody.collisionDetectionMode;
        public RigidbodyConstraints Constraints => _rigidbody.constraints;
        public bool DetectCollisions => _rigidbody.detectCollisions;
        public float Drag => _rigidbody.drag;
        public bool FreezeRotation => _rigidbody.freezeRotation;
        public Vector3 InertiaTensor => _rigidbody.inertiaTensor;
        public Quaternion InertiaTensorRotation => _rigidbody.inertiaTensorRotation;
        public RigidbodyInterpolation Interpolation => _rigidbody.interpolation;
        public bool IsKinematic => _rigidbody.isKinematic;
        public float Mass => _rigidbody.mass;
        public float MaxAngularVelocity => _rigidbody.maxAngularVelocity;
        public float MaxDepenetrationVelocity => _rigidbody.maxDepenetrationVelocity;
        public float SleepThreshold => _rigidbody.sleepThreshold;
        public float SolverIterations => _rigidbody.solverIterations;
        public float SolverVelocityIterations => _rigidbody.solverVelocityIterations;
        public bool UseGravity => _rigidbody.useGravity;
        public Vector3 Velocity => _rigidbody.velocity;
        public Vector3 WorldCenterOfMass => _rigidbody.worldCenterOfMass;
    }
}