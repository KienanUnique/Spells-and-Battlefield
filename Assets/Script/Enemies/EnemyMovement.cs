using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _updateDestinationCooldownSeconds;
        // TODO: disable moving with navmesh, use it only to rotate and calculate desiredVelocity
        private NavMeshAgent _navMeshAgent;
        private Rigidbody _rigidbody;
        private Coroutine _currentActionCoroutine = null;
        public Vector3 CurrentPosition => _rigidbody.position;

        public void StartMovingToTarget(Transform target)
        {
            if (_currentActionCoroutine == null)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.updateRotation = true;
                _currentActionCoroutine = StartCoroutine(MoveTowardsTarget(target));
            }
            else
            {
                StopCurrentAction();
            }
        }

        public void StartMovingWithRotatingTowardsTarget(Transform target)
        {
            if (_currentActionCoroutine == null)
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.updateRotation = false;
                _currentActionCoroutine = StartCoroutine(MovingWithRotatingTowardsTarget(target));
            }
            else
            {
                StopCurrentAction();
            }
        }

        public void StopCurrentAction()
        {
            if (_currentActionCoroutine != null)
            {
                StopCoroutine(_currentActionCoroutine);
                _currentActionCoroutine = null;
                _navMeshAgent.velocity = Vector3.zero;
                _navMeshAgent.isStopped = true;
                _navMeshAgent.updateRotation = false;
            }
        }
        
        public void AddForce(Vector3 force, ForceMode mode)
        {
            _navMeshAgent.enabled = false;
            _rigidbody.AddForce(force, mode);
            _navMeshAgent.enabled = true;
        }
        
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private IEnumerator MoveTowardsTarget(Transform target)
        {
            while (true)
            {
                _navMeshAgent.SetDestination(target.position);
                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator MovingWithRotatingTowardsTarget(Transform target)
        {
            while (true)
            {
                var targetPosition = target.position;
                _navMeshAgent.SetDestination(targetPosition);

                var direction = (targetPosition - transform.position).normalized;
                var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = lookRotation;

                yield return null;
            }
        }
    }
}