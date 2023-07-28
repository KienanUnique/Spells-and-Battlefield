using System.Collections;
using Enemies.Movement.Setup_Data;
using General_Settings_in_Scriptable_Objects.Sections;
using Settings.Sections.Movement;
using UnityEngine;

namespace Enemies.Movement.Concrete_Types.Movement_With_Gravity
{
    public class EnemyMovementWithGravity : EnemyMovementBase
    {
        private readonly MovementOnGroundSettingsSection _movementSettings;

        public EnemyMovementWithGravity(IEnemyMovementSetupData setupData,
            MovementOnGroundSettingsSection movementSettings, TargetPathfinderSettingsSection targetPathfinderSettings)
            : base(setupData, movementSettings, targetPathfinderSettings)
        {
            _movementSettings = movementSettings;
            _coroutineStarter.StartCoroutine(ApplyGravityContinuously());
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