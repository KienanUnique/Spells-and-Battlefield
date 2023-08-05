using System;
using System.Collections;
using Common;
using Common.Abstract_Bases.Movement;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Enemies.Movement.Enemy_Data_For_Moving;
using Enemies.Movement.Setup_Data;
using General_Settings_in_Scriptable_Objects.Sections;
using Interfaces;
using Settings.Sections.Movement;
using UnityEngine;

namespace Enemies.Movement
{
    public abstract class EnemyMovementBase : MovementBase, IDisableableEnemyMovement
    {
        private const RigidbodyConstraints RigidbodyConstraintsFreezeRotationAndPositionXY =
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotation;

        protected readonly ICoroutineStarter _coroutineStarter;
        private readonly ValueWithReactionOnChange<bool> _isMoving;
        private readonly TargetPathfinder _targetPathfinder;
        private readonly Transform _cachedTransform;
        private readonly Transform _originalParent;
        private Coroutine _followPathCoroutine;

        protected EnemyMovementBase(IEnemyMovementSetupData setupData,
            MovementSettingsSection movementSettings, TargetPathfinderSettingsSection targetPathfinderSettings) :
            base(setupData.Rigidbody, movementSettings)
        {
            _coroutineStarter = setupData.CoroutineStarter;
            ReadonlyRigidbody = new ReadonlyRigidbody(_rigidbody);
            _cachedTransform = _rigidbody.transform;
            _originalParent = _cachedTransform.parent;
            _isMoving = new ValueWithReactionOnChange<bool>(false);
            _targetPathfinder =
                new TargetPathfinder(setupData.Seeker, targetPathfinderSettings, _coroutineStarter);
        }

        public event Action<bool> MovingStateChanged;
        public Vector3 CurrentPosition => _rigidbody.position;
        protected virtual Vector3 VelocityForLimitations => _rigidbody.velocity;
        public IReadonlyRigidbody ReadonlyRigidbody { get; }

        public void StartKeepingTransformOnDistance(IReadonlyTransform target, IEnemyDataForMoving dataForMoving)
        {
            if (_followPathCoroutine != null)
            {
                StopMoving();
            }

            _isMoving.Value = true;
            _followPathCoroutine =
                _coroutineStarter.StartCoroutine(KeepingTransformOnDistance(target,
                    dataForMoving.NeedDistanceFromTarget));
        }

        public void StopMoving()
        {
            if (_followPathCoroutine != null)
            {
                _coroutineStarter.StopCoroutine(_followPathCoroutine);
                _followPathCoroutine = null;
            }

            _isMoving.Value = false;
            _targetPathfinder.StopUpdatingPath();
            _rigidbody.velocity = Vector3.zero;
        }

        public void DisableMoving()
        {
            StopMoving();
            _rigidbody.constraints = RigidbodyConstraintsFreezeRotationAndPositionXY;
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _rigidbody.AddForce(force, mode);
        }

        public void StickToPlatform(Transform platformTransform)
        {
            _cachedTransform.SetParent(platformTransform);
        }

        public void UnstickFromPlatform()
        {
            _cachedTransform.SetParent(_originalParent);
        }

        protected override void SubscribeOnEvents()
        {
            SubscribeOnThisEvents();
        }

        protected override void UnsubscribeFromEvents()
        {
            UnsubscribeFromThisEvents();
        }

        private void SubscribeOnThisEvents()
        {
            _isMoving.AfterValueChanged += OnIsMovingStatusChanged;
        }

        private void UnsubscribeFromThisEvents()
        {
            _isMoving.AfterValueChanged -= OnIsMovingStatusChanged;
        }

        private void OnIsMovingStatusChanged(bool b)
        {
            MovingStateChanged?.Invoke(b);
        }

        private IEnumerator KeepingTransformOnDistance(IReadonlyTransform targetPosition, float needDistance)
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            _targetPathfinder.StartUpdatingPathForKeepingTransformOnDistance(targetPosition, needDistance);
            var direction = Vector3.zero;
            while (true)
            {
                if (_targetPathfinder.TryGetNextWaypoint(out var waypointPosition))
                {
                    SetDirectionTowardsPoint(waypointPosition, ref direction);
                    _rigidbody.AddForce(MovementSettings.MoveForce * Time.deltaTime * _currentSpeedRatio * direction);
                    ApplyFriction(direction);
                    TryLimitCurrentSpeed();
                }
                else
                {
                    SetDirectionTowardsPoint(targetPosition.Position, ref direction);
                }

                yield return waitForFixedUpdate;
            }
        }

        private void SetDirectionTowardsPoint(Vector3 needPoint, ref Vector3 direction)
        {
            direction = needPoint - _rigidbody.position;
            direction.y = 0;
            direction = direction.normalized;
        }

        private void ApplyFriction(Vector3 needMoveDirection)
        {
            var currentVelocity = VelocityForLimitations;
            var needFrictionDirection = Time.deltaTime * _currentSpeedRatio *
                                        MovementSettings.NormalFrictionCoefficient *
                                        MovementSettings.MoveForce * currentVelocity.magnitude *
                                        (needMoveDirection - currentVelocity.normalized);
            _rigidbody.AddForce(needFrictionDirection);
        }
    }
}