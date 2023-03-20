using Enemies.State_Machine;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(IdHolder))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(Character))]
    public abstract class EnemyControllerBase : MonoBehaviour, IEnemy, IEnemyStateMachineControllable
    {
        public int Id => _idHolder.Id;

        public IEnemyTarget Target { get; set; }

        protected abstract EnemyVisualBase EnemyVisual { get; }
        protected IdHolder _idHolder;
        protected Character _character;
        protected NavMeshAgent _navMeshAgent;
        protected EnemyMovement _enemyMovement;
        [SerializeField] protected EnemyStateMachineAI _enemyStateMachineAI;

        [SerializeField] private PlayerController _player;

        public int CompareTo(object obj)
        {
            return _idHolder.CompareTo(obj);
        }

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
            _character = GetComponent<Character>();
            Target = _player;
        }

        protected virtual void Start()
        {
            _enemyStateMachineAI.StartStateMachine(this);
        }

        protected virtual void Update()
        {
            EnemyVisual.UpdateMovingData(!_navMeshAgent.isStopped);
        }
    }
}