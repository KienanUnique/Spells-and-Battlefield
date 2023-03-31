using Enemies.State_Machine;
using Interfaces;
using Pickable_Items;
using Player;
using Spells;
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
        [SerializeField] protected PickableSpellController _pickableSpellPrefab;
        [SerializeField] protected SpellBase _spellToDrop;

        [SerializeField] private PlayerController _player;
        private readonly Vector3 _spawnSpellOffset = new Vector3(0, 3f, 0);

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

        public void ApplyContinuousEffect(IContinuousEffect effect)
        {
            _character.ApplyContinuousEffect(effect);
        }

        public void StartMovingToTarget(Transform target) => _enemyMovement.StartMovingToTarget(target);

        public void StopMovingToTarget() => _enemyMovement.StopCurrentAction();

        protected virtual void HandleCharacterStateChangedEvent(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _enemyStateMachineAI.StopStateMachine();
                _enemyMovement.StopCurrentAction();
                DropSpell();
            }
        }

        protected virtual void OnEnable()
        {
            _character.CharacterStateChanged += HandleCharacterStateChangedEvent;
        }

        protected virtual void OnDisable()
        {
            _character.CharacterStateChanged -= HandleCharacterStateChangedEvent;
        }

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

        private void DropSpell()
        {
            var localTransform = transform;
            var dropDirection = Target == null
                ? localTransform.forward
                : (Target.MainTransform.position - localTransform.position).normalized;
            var spawnPosition = _spawnSpellOffset + localTransform.position;
            var pickableSpellController =
                Instantiate(_pickableSpellPrefab.gameObject, spawnPosition, Quaternion.identity)
                    .GetComponent<PickableSpellController>();
            pickableSpellController.DropItem(_spellToDrop, dropDirection);
        }
    }
}