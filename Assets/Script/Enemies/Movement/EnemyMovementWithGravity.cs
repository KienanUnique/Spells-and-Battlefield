using System.Collections;
using General_Settings_in_Scriptable_Objects.Sections;
using Interfaces;
using Pathfinding;
using Settings.Sections.Movement;
using UnityEngine;

namespace Enemies.Movement
{
    public class EnemyMovementWithGravity : EnemyMovement
    {
        private readonly MovementOnGroundSettingsSection _movementSettings;

        public EnemyMovementWithGravity(ICoroutineStarter coroutineStarter,
            MovementOnGroundSettingsSection movementSettings,
            TargetPathfinderSettingsSection targetPathfinderSettings, Seeker seeker, Rigidbody rigidbody) : base(
            coroutineStarter, movementSettings, targetPathfinderSettings, seeker, rigidbody)
        {
            _movementSettings = movementSettings;
            coroutineStarter.StartCoroutine(ApplyGravityContinuously());
        }

        protected override Vector3 VelocityForLimitations
        {
            get
            {
                var velocity = _rigidbody.velocity;
                velocity.Set(velocity.x, 0f, velocity.z);
                return velocity;
            }
        }

        private IEnumerator ApplyGravityContinuously()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                ApplyGravity(_movementSettings.NormalGravityForce);
                yield return waitForFixedUpdate;
            }
        }

        protected override void TryLimitCurrentSpeed()
        {
            var velocityForLimitations = VelocityForLimitations;
            if (velocityForLimitations.magnitude > CurrentMaximumSpeed)
            {
                var resultVelocity = velocityForLimitations.normalized * CurrentMaximumSpeed;
                resultVelocity.y += _rigidbody.velocity.y;
                _rigidbody.velocity = resultVelocity;
            }
            else if (velocityForLimitations.magnitude < StopVelocityMagnitude)
            {
                _rigidbody.velocity = Vector3.zero;
            }
        }
    }
}