using Enemies.State_Machine;
using Interfaces;
using Pickable_Items;
using Player;
using Spells;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(IdHolder))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(Character))]
    public abstract class EnemyControllerBase : MonoBehaviour, IEnemy, IEnemyStateMachineControllable
    {
        public int Id => _idHolder.Id;
        public Vector3 CurrentPosition => _enemyMovement.CurrentPosition;
        public IEnemyTarget Target { get; private set; }

        protected abstract EnemyVisualBase EnemyVisual { get; }
        protected IdHolder _idHolder;
        protected Character _character;
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

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _enemyMovement.AddForce(force, mode);
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
            _enemyMovement.IsMovingStateChanged += EnemyVisual.UpdateMovingData;
        }

        protected virtual void OnDisable()
        {
            _character.CharacterStateChanged -= HandleCharacterStateChangedEvent;
            _enemyMovement.IsMovingStateChanged -= EnemyVisual.UpdateMovingData;
        }

        protected virtual void Awake()
        {
            _idHolder = GetComponent<IdHolder>();
            _enemyMovement = GetComponent<EnemyMovement>();
            _character = GetComponent<Character>();
            Target = _player;
        }

        protected virtual void Start()
        {
            _enemyStateMachineAI.StartStateMachine(this);
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