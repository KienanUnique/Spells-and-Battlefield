using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IdHolder))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStateMachineAI))]
public class EnemyController : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyVisual _enemyVisual;
    [SerializeField] private PlayerController _target;
    private IdHolder _idHolder;
    private Character _character;
    private NavMeshAgent _navMeshAgent;
    private EnemyStateMachineAI _enemyStateMachineAI;

    public int Id => _idHolder.Id;

    private void Awake()
    {
        _idHolder = GetComponent<IdHolder>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemyStateMachineAI = GetComponent<EnemyStateMachineAI>();
        _character = new Character();
    }

    private void Start()
    {
        _enemyStateMachineAI.StartStateMachine(_target);
    }

    private void Update()
    {
        _enemyVisual.UpdateMovingData(!_navMeshAgent.isStopped);
    }

    public void HandleHeal(int countOfHealthPoints)
    {
        _character.HandleHeal(countOfHealthPoints);
    }

    public void HandleDamage(int countOfHealthPoints)
    {
        _character.HandleDamage(countOfHealthPoints);
    }
}
