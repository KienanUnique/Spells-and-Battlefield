using System;
using System.Collections;
using Common;
using Common.Abstract_Bases.Movement;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Enemies.Movement.Enemy_Data_For_Moving;
using Enemies.Movement.Setup_Data;
using Enemies.Target_Pathfinder;
using Enemies.Target_Selector_From_Triggers;
using Interfaces;
using Settings.Sections;
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
        private readonly ITargetPathfinderForMovement _targetPathfinder;
        private readonly Transform _cachedTransform;
        private readonly Transform _originalParent;
        private readonly IReadonlyEnemyTargetFromTriggersSelector _targetSelector;
        private Coroutine _followPathCoroutine;
        private IEnemyDataForMoving _currentDataForMoving;
        private bool _needMove;

        protected EnemyMovementBase(IEnemyMovementSetupData setupData,
            MovementSettingsSection movementSettings) :
            base(setupData.Rigidbody, movementSettings)
        {
            _coroutineStarter = setupData.CoroutineStarter;
            ReadonlyRigidbody = new ReadonlyRigidbody(_rigidbody);
            _cachedTransform = _rigidbody.transform;
            _originalParent = _cachedTransform.parent;
            _isMoving = new ValueWithReactionOnChange<bool>(false);
            _targetPathfinder = setupData.TargetPathfinderForMovement;
            _targetSelector = setupData.TargetSelector;
        }

        public event Action<bool> MovingStateChanged;
        public Vector3 CurrentPosition => _rigidbody.position;
        public IReadonlyRigidbody ReadonlyRigidbody { get; }
        protected virtual Vector3 VelocityForLimitations => _rigidbody.velocity;
        private IEnemyTarget CurrentTarget => _targetSelector.CurrentTarget;

        public void StartKeepingCurrentTargetOnDistance(IEnemyDataForMoving dataForMoving)
        {
            _currentDataForMoving = dataForMoving;

            if (_followPathCoroutine != null)
            {
                StopMoving();
            }

            _needMove = true;

            if (CurrentTarget != null)
            {
                _isMoving.Value = true;
                _followPathCoroutine =
                    _coroutineStarter.StartCoroutine(KeepingTransformOnDistance(CurrentTarget.MainRigidbody,
                        dataForMoving.NeedDistanceFromTarget));
            }
        }

        public void StopMoving()
        {
            if (_followPathCoroutine != null)
            {
                _coroutineStarter.StopCoroutine(_followPathCoroutine);
                _followPathCoroutine = null;
            }

            _needMove = false;
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
            _targetSelector.CurrentTargetChanged += OnCurrentTargetChanged;
            _isMoving.AfterValueChanged += OnIsMovingStatusChanged;
        }

        private void UnsubscribeFromThisEvents()
        {
            _targetSelector.CurrentTargetChanged -= OnCurrentTargetChanged;
            _isMoving.AfterValueChanged -= OnIsMovingStatusChanged;
        }

        private void OnCurrentTargetChanged(IEnemyTarget oldTarget, IEnemyTarget newTarget)
        {
            if (!_needMove)
            {
                return;
            }

            StartKeepingCurrentTargetOnDistance(_currentDataForMoving);
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
                if (_targetPathfinder.IsPathComplete())
                {
                    SetDirectionTowardsPoint(targetPosition.Position, ref direction);
                    _rigidbody.velocity = Vector3.zero;
                }
                else
                {
                    SetDirectionTowardsPoint(_targetPathfinder.CurrentWaypoint, ref direction);
                    _rigidbody.AddForce(MovementSettings.MoveForce * Time.deltaTime * _currentSpeedRatio * direction);
                    ApplyFriction(direction);
                    TryLimitCurrentSpeed();
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