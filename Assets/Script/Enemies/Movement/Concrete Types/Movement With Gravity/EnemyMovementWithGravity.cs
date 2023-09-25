using System.Collections;
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
                Vector3 velocity = _rigidbody.velocity;
                velocity.Set(velocity.x, 0f, velocity.z);
                return velocity;
            }
        }

        protected override void TryLimitCurrentSpeed()
        {
            Vector3 velocityForLimitations = VelocityForLimitations;
            if (velocityForLimitations.magnitude > MovementValuesCalculator.MaximumSpeedCalculated)
            {
                Vector3 resultVelocity = velocityForLimitations.normalized *
                                         MovementValuesCalculator.MaximumSpeedCalculated;
                resultVelocity.y += _rigidbody.velocity.y;
                _rigidbody.velocity = resultVelocity;
            }
            else if (velocityForLimitations.magnitude < StopVelocityMagnitude)
            {
                _rigidbody.velocity = Vector3.zero;
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
    }
}