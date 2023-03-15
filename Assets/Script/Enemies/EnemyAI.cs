using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private float _updateDestinationCooldownSeconds;
    private NavMeshAgent _navMeshAgent;
    private Transform _playerTransform;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerTransform = _player.transform;
    }

    private void Start()
    {
        StartCoroutine(UpdateDestinationWithCooldown());
    }

    private IEnumerator UpdateDestinationWithCooldown()
    {
        while (true)
        {
            _navMeshAgent.destination = _playerTransform.position;
            yield return new WaitForSecondsRealtime(_updateDestinationCooldownSeconds);
        }
    }


}
