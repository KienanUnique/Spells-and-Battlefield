using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPathfinder : MonoBehaviour
{
    [SerializeField] private float _updateDestinationCooldownSeconds;
    private NavMeshAgent _navMeshAgent;
    private Coroutine _currentCoroutine = null;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void StartMovingToTarget(Transform target)
    {
        if (_currentCoroutine == null)
        {
            _navMeshAgent.isStopped = false;
            _currentCoroutine = StartCoroutine(UpdateDestinationWithCooldown(target));
        }
    }

    public void StopMovingToTarget()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
            _navMeshAgent.isStopped = true;
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


}
