using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _updateDestinationCooldownSeconds;
    [SerializeField] private float _rotationSpeed;
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
            _currentActionCoroutine = StartCoroutine(UpdateDestinationWithCooldown(target));
        }
        else
        {
            StopCurrentAction();
        }
    }

    public void StartRotatingTowardsTarget(Transform target)
    {
        if (_currentActionCoroutine == null)
        {
            _navMeshAgent.velocity = Vector3.zero;
            _navMeshAgent.isStopped = true;
            _currentActionCoroutine = StartCoroutine(RotatingTowardsTarget(target));
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

    private IEnumerator RotatingTowardsTarget(Transform target)
    {
        while (true)
        {
            transform.LookAt(target);
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
