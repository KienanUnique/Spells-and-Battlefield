using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(TargetPathfinder))]
    public class EnemyMovement : MovementBase
    {
        private ValueWithReactionOnChange<bool> _isMoving;
        private TargetPathfinder _targetPathfinder;
        private Coroutine _followPathCoroutine = null;
        public event Action<bool> IsMovingStateChanged;
        public Vector3 CurrentPosition => _rigidbody.position;

        public void StartMovingToTarget(Transform target)
        {
            if (_followPathCoroutine != null)
            {
                StopMovingToTarget();
            }

            _isMoving.Value = true;
            _followPathCoroutine = StartCoroutine(FollowPath(target));
        }

        public void StopMovingToTarget()
        {
            if (_followPathCoroutine != null)
            {
                StopCoroutine(_followPathCoroutine);
                _followPathCoroutine = null;
            }

            _isMoving.Value = false;
            _targetPathfinder.StopUpdatingPath();
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _rigidbody.AddForce(force, mode);
        }

        protected override void SpecialAwakeAction()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _targetPathfinder = GetComponent<TargetPathfinder>();
            _isMoving = new ValueWithReactionOnChange<bool>(false);
        }

        private void OnEnable()
        {
            _isMoving.AfterValueChanged += b => IsMovingStateChanged?.Invoke(b);
        }

        private void OnDisable()
        {
            _isMoving.AfterValueChanged -= b => IsMovingStateChanged?.Invoke(b);
        }

        private IEnumerator FollowPath(Transform target)
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            _targetPathfinder.StartUpdatingPathForTarget(target);
            var direction = Vector3.zero;
            while (true)
            {
                if (_targetPathfinder.TryGetNextWaypoint(out var waypointPosition))
                {
                    SetDirectionTowardsPoint(waypointPosition, ref direction);
                    _rigidbody.AddForce(_moveForce * Time.deltaTime * _currentSpeedRatio * direction);
                    ApplyFriction(direction);
                    TryLimitCurrentSpeed();
                }
                else
                {
                    SetDirectionTowardsPoint(target.position, ref direction);
                }

                if (direction != Vector3.zero)
                {
                    _rigidbody.rotation = Quaternion.LookRotation(direction, Vector3.up);
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
            var currentVelocity = _rigidbody.velocity;
            var needFrictionDirection = Time.deltaTime * _currentSpeedRatio * _frictionCoefficient * _moveForce *
                                        currentVelocity.magnitude * (needMoveDirection - currentVelocity.normalized);
            _rigidbody.AddForce(needFrictionDirection);
        }
    }
}