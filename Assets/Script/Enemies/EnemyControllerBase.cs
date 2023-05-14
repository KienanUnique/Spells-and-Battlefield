using System;
using System.Collections;
using Enemies.State_Machine;
using General_Settings_in_Scriptable_Objects;
using Interfaces;
using Pickable_Items;
using Spells;
using Spells.Continuous_Effect;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;
using Zenject;

namespace Enemies
{
    [RequireComponent(typeof(IdHolder))]
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(Character))]
    public abstract class EnemyControllerBase : MonoBehaviour, IEnemy, IEnemyStateMachineControllable
    {
        protected IdHolder _idHolder;
        protected Character _character;
        protected EnemyMovement _enemyMovement;
        [SerializeField] protected EnemyStateMachineAI _enemyStateMachineAI;
        [SerializeField] protected SpellScriptableObject _spellToDrop;
        private GeneralEnemySettings _generalEnemySettings;
        private IPickableSpellsFactory _spellsFactory;

        [Inject]
        private void Construct(GeneralEnemySettings generalEnemySettings, IEnemyTarget enemyTarget,
            IPickableSpellsFactory spellsFactory)
        {
            _generalEnemySettings = generalEnemySettings;
            Target = enemyTarget; // TODO: Add enemy trigger zone from which Target will be got
            _spellsFactory = spellsFactory;
        }

        public event Action<float> HitPointsCountChanged;

        public float HitPointCountRatio => _character.HitPointCountRatio;
        public int Id => _idHolder.Id;
        public Vector3 CurrentPosition => _enemyMovement.CurrentPosition;
        public ValueWithReactionOnChange<CharacterState> CurrentCharacterState => _character.CurrentState;
        public IEnemyTarget Target { get; private set; }
        protected abstract EnemyVisualBase EnemyVisual { get; }
        protected abstract IEnemySettings EnemySettings { get; }

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

        protected virtual void OnEnable()
        {
            _character.StateChanged += OnStateChanged;
            _character.HitPointsCountChanged += OnHitPointsCountChanged;
            _enemyMovement.MovingStateChanged += EnemyVisual.UpdateMovingData;
        }

        protected virtual void OnDisable()
        {
            _character.StateChanged -= OnStateChanged;
            _character.HitPointsCountChanged -= OnHitPointsCountChanged;
            _enemyMovement.MovingStateChanged -= EnemyVisual.UpdateMovingData;
        }

        protected virtual void Awake()
        {
            _idHolder = GetComponent<IdHolder>();
            _enemyMovement = GetComponent<EnemyMovement>();
            _character = GetComponent<Character>();
            _enemyMovement.Initialize(EnemySettings.MovementSettings, EnemySettings.TargetPathfinderSettingsSection);
        }

        protected virtual void Start()
        {
            _enemyStateMachineAI.StartStateMachine(this);
        }

        private void OnStateChanged(CharacterState newState)
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

        private void OnHitPointsCountChanged(float newHitPointsCount) =>
            HitPointsCountChanged?.Invoke(newHitPointsCount);

        private void DropSpell()
        {
            var cashedTransform = transform;
            var dropDirection = Target == null
                ? cashedTransform.forward
                : (Target.MainTransform.position - cashedTransform.position).normalized;
            var spawnPosition = _generalEnemySettings.SpawnSpellOffset + cashedTransform.position;
            var pickableSpell = _spellsFactory.Create(_spellToDrop.GetImplementationObject(), spawnPosition);
            pickableSpell.DropItemTowardsDirection(dropDirection);
        }

        private IEnumerator DestroyAfterDelay()
        {
            yield return new WaitForSeconds(_generalEnemySettings.DelayInSecondsBeforeDestroy);
            Destroy(this.gameObject);
        }
    }
}