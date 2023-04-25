using System.Collections;
using Enemies.State_Machine;
using Game_Managers;
using General_Settings_in_Scriptable_Objects;
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
        protected EnemySettings _settings;
        protected IdHolder _idHolder;
        protected Character _character;
        protected EnemyMovement _enemyMovement;
        [SerializeField] protected EnemyStateMachineAI _enemyStateMachineAI;
        [SerializeField] protected SpellBase _spellToDrop;
        [SerializeField] private PlayerController _player;
        

        public int Id => _idHolder.Id;
        public Vector3 CurrentPosition => _enemyMovement.CurrentPosition;
        public ValueWithReactionOnChange<CharacterState> CurrentCharacterState => _character.CurrentState;
        public IEnemyTarget Target { get; private set; }
        protected abstract EnemyVisualBase EnemyVisual { get; }

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

        public void StopMovingToTarget() => _enemyMovement.StopMovingToTarget();

        public void MultiplySpeedRatioBy(float speedRatio)
        {
            _enemyMovement.MultiplySpeedRatioBy(speedRatio);
        }

        public void DivideSpeedRatioBy(float speedRatio)
        {
            _enemyMovement.DivideSpeedRatioBy(speedRatio);
        }

        protected virtual void HandleStateChangedEvent(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _enemyStateMachineAI.StopStateMachine();
                _enemyMovement.StopMovingToTarget();
                EnemyVisual.PlayDieAnimation();
                DropSpell();
                StartCoroutine(DestroyAfterDelay());
            }
        }

        protected virtual void OnEnable()
        {
            _character.StateChanged += HandleStateChangedEvent;
            _enemyMovement.IsMovingStateChanged += EnemyVisual.UpdateMovingData;
        }

        protected virtual void OnDisable()
        {
            _character.StateChanged -= HandleStateChangedEvent;
            _enemyMovement.IsMovingStateChanged -= EnemyVisual.UpdateMovingData;
        }

        protected virtual void Awake()
        {
            _idHolder = GetComponent<IdHolder>();
            _enemyMovement = GetComponent<EnemyMovement>();
            _character = GetComponent<Character>();
            _settings = SettingsProvider.Instance.EnemySettings;
            Target = _player;
        }

        protected virtual void Start()
        {
            _enemyStateMachineAI.StartStateMachine(this);
        }

        private void DropSpell()
        {
            var cashedTransform = transform;
            var dropDirection = Target == null
                ? cashedTransform.forward
                : (Target.MainTransform.position - cashedTransform.position).normalized;
            var spawnPosition = _settings.SpawnSpellOffset + cashedTransform.position;
            var pickableSpellController =
                Instantiate(_settings.PickableSpellPrefab.gameObject, spawnPosition, Quaternion.identity)
                    .GetComponent<PickableSpellController>();
            pickableSpellController.DropItem(_spellToDrop, dropDirection);
        }

        private IEnumerator DestroyAfterDelay()
        {
            yield return new WaitForSeconds(_settings.DelayInSecondsBeforeDestroy);
            Destroy(this.gameObject);
        }
    }
}