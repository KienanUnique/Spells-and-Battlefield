using System.Collections;
using Common.Settings.Sections.Movement;
using Common.Settings.Sections.Movement.Movement_With_Gravity;
using Enemies.Movement.Setup_Data;
using UnityEngine;

namespace Enemies.Movement.Concrete_Types.Movement_With_Gravity
{
    public class EnemyMovementWithGravity : EnemyMovementBase
    {
        private readonly IMovementOnGroundSettingsSection _movementSettings;

        public EnemyMovementWithGravity(IEnemyMovementSetupData setupData,
            IMovementOnGroundSettingsSection movementSettings) : base(setupData, movementSettings)
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