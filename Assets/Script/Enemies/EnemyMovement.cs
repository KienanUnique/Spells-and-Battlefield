using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _updateDestinationCooldownSeconds;
    private NavMeshAgent _navMeshAgent;
    private Coroutine _currentActionCoroutine = null;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void StartMovingToTarget(Transform target)
    {
        if (_currentActionCoroutine == null)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.updateRotation = true;
            _currentActionCoroutine = StartCoroutine(UpdateDestinationWithCooldown(target));
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
        else
        {
            throw new CanNotStopNullMovementActionException();
        }
    }

    private IEnumerator UpdateDestinationWithCooldown(Transform target)
    {
        while (true)
        {
            _navMeshAgent.SetDestination(target.position);
            yield return new WaitForSecondsRealtime(_updateDestinationCooldownSeconds);
        }
    }

    private IEnumerator MovingWithRotatingTowardsTarget(Transform target)
    {
        while (true)
        {
            _navMeshAgent.SetDestination(target.position);

            var direction = (target.position - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = lookRotation;

            yield return null;
        }
    }

    private class SomeMovementActionIsAlreadyRunningException : Exception
    {
        public SomeMovementActionIsAlreadyRunningException() : base("Some movement action is already running")
        {
        }
    }

    private class CanNotStopNullMovementActionException : Exception
    {
        public CanNotStopNullMovementActionException() : base("Can't stop null movement action")
        {
        }
    }
}
