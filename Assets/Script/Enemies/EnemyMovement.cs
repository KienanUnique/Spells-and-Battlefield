using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Seeker))]
    [RequireComponent(typeof(TargetPathfinder))]
    public class EnemyMovement : MonoBehaviour
    {
        public Vector3 CurrentPosition => _rigidbody.position;
        public event Action<bool> IsMovingStateChanged;
        [SerializeField] private float _runVelocity;
        private ValueWithReactionOnChange<bool> _isMoving;
        private Rigidbody _rigidbody;
        private TargetPathfinder _targetPathfinder;
        private Coroutine _currentActionCoroutine = null;

        public void StartMovingToTarget(Transform target)
        {
            if (_currentActionCoroutine != null)
            {
                StopCurrentAction();
            }

            _currentActionCoroutine = StartCoroutine(FollowPath(target));
        }

        public void StopCurrentAction()
        {
            if (_currentActionCoroutine != null)
            {
                StopCoroutine(_currentActionCoroutine);
                _currentActionCoroutine = null;
            }
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _rigidbody.AddForce(force, mode);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _targetPathfinder = GetComponent<TargetPathfinder>();
            _isMoving = new ValueWithReactionOnChange<bool>(false);
        }

        private void OnEnable()
        {
            _isMoving.ValueChanged += b => IsMovingStateChanged?.Invoke(b);
        }

        private void OnDisable()
        {
            _isMoving.ValueChanged -= b => IsMovingStateChanged?.Invoke(b);
        }

        private IEnumerator FollowPath(Transform target)
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            _targetPathfinder.UpdatePathForTarget(target);
            var direction = Vector3.zero;
            var currentHorizontalVelocity = Vector3.zero;
            Vector3 currentHorizontalVelocityNormalized, needVelocity;
            while (true)
            {
                _isMoving.Value = _targetPathfinder.TryGetNextWaypoint(out var waypointPosition);
                if (_isMoving.Value)
                {
                    SetDirectionTowardsPoint(waypointPosition, ref direction);
                    needVelocity = direction * _runVelocity;
                    currentHorizontalVelocity.x = _rigidbody.velocity.x;
                    currentHorizontalVelocity.z = _rigidbody.velocity.z;
                    currentHorizontalVelocityNormalized = currentHorizontalVelocity.normalized;

                    if (direction.magnitude >= (direction - currentHorizontalVelocityNormalized).magnitude)
                    {
                        needVelocity -= currentHorizontalVelocity;
                    }

                    _rigidbody.AddForce(needVelocity, ForceMode.VelocityChange);
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
    }
}