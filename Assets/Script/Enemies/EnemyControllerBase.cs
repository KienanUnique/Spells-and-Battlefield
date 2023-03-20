using Enemies.State_Machine;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(IdHolder))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyStateMachineAI))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(Character))]
    public abstract class EnemyControllerBase : MonoBehaviour, IEnemy
    {
        public int Id => _idHolder.Id;

        protected abstract EnemyVisualBase EnemyVisual { get; }

        [SerializeField] protected PlayerController _target;
        protected IdHolder _idHolder;
        protected Character _character;
        protected NavMeshAgent _navMeshAgent;
        protected EnemyMovement _enemyMovement;
        protected EnemyStateMachineAI _enemyStateMachineAI;

        public virtual void HandleHeal(int countOfHealthPoints)
        {
            _character.HandleHeal(countOfHealthPoints);
        }

        public virtual void HandleDamage(int countOfHealthPoints)
        {
            _character.HandleDamage(countOfHealthPoints);
        }

        public void StartMovingToTarget(Transform target) => _enemyMovement.StartMovingToTarget(target);

        public void StopMovingToTarget() => _enemyMovement.StopCurrentAction();

        protected virtual void Awake()
        {
            _idHolder = GetComponent<IdHolder>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyStateMachineAI = GetComponent<EnemyStateMachineAI>();
            _character = GetComponent<Character>();
        }

        protected virtual void Start()
        {
            _enemyStateMachineAI.StartStateMachine(_target);
        }

        protected virtual void Update()
        {
            EnemyVisual.UpdateMovingData(!_navMeshAgent.isStopped);
        }
    }
}