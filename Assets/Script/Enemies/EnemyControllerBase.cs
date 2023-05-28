using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Character;
using Enemies.State_Machine;
using Enemies.Target_Selector;
using Enemies.Trigger;
using General_Settings_in_Scriptable_Objects;
using Interfaces;
using Pathfinding;
using Pickable_Items;
using Settings;
using Spells.Continuous_Effect;
using Spells.Spell.Scriptable_Objects;
using UnityEngine;
using Zenject;

namespace Enemies
{
    [RequireComponent(typeof(IdHolder))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Seeker))]
    public abstract class EnemyControllerBase : MonoBehaviour, IEnemy, IEnemyStateMachineControllable,
        ICoroutineStarter, IEnemyTriggersSettable
    {
        [SerializeField] protected EnemyStateMachineAI _enemyStateMachineAI;
        [SerializeField] protected SpellScriptableObject _spellToDrop;
        [SerializeField] private List<EnemyTargetTrigger> _targetTriggers;
        protected EnemyMovement _enemyMovement;
        private List<IDisableable> _itemsNeedDisabling;
        private IdHolder _idHolder;
        private GeneralEnemySettings _generalEnemySettings;
        private IPickableSpellsFactory _spellsFactory;
        private EnemyTargetSelector _targetSelector;

        [Inject]
        private void Construct(GeneralEnemySettings generalEnemySettings, IPickableSpellsFactory spellsFactory)
        {
            _generalEnemySettings = generalEnemySettings;
            _spellsFactory = spellsFactory;
        }

        public event Action<float> HitPointsCountChanged;

        public float HitPointCountRatio => Character.HitPointCountRatio;
        public int Id => _idHolder.Id;
        public Vector3 CurrentPosition => _enemyMovement.CurrentPosition;
        public ValueWithReactionOnChange<CharacterState> CurrentCharacterState => Character.CurrentState;
        protected abstract EnemyVisualBase EnemyVisual { get; }
        protected abstract IEnemySettings EnemySettings { get; }
        protected abstract CharacterBase Character { get; }

        public void SetExternalEnemyTargetTriggers(List<IEnemyTargetTrigger> enemyTargetTriggers)
        {
            enemyTargetTriggers.ForEach(trigger => _targetSelector.AddTrigger(trigger));
        }

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
            Character.HandleHeal(countOfHealthPoints);
        }

        public virtual void HandleDamage(int countOfHealthPoints)
        {
            Character.HandleDamage(countOfHealthPoints);
        }

        public void ApplyContinuousEffect(IAppliedContinuousEffect effect)
        {
            Character.ApplyContinuousEffect(effect);
        }

        public IEnemyTargetSelector TargetSelector => _targetSelector;
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
            _itemsNeedDisabling.ForEach(item => item.Enable());
            Character.StateChanged += OnStateChanged;
            Character.HitPointsCountChanged += OnHitPointsCountChanged;
            _enemyMovement.MovingStateChanged += EnemyVisual.UpdateMovingData;
        }

        protected virtual void OnDisable()
        {
            _itemsNeedDisabling.ForEach(item => item.Disable());
            Character.StateChanged -= OnStateChanged;
            Character.HitPointsCountChanged -= OnHitPointsCountChanged;
            _enemyMovement.MovingStateChanged -= EnemyVisual.UpdateMovingData;
        }

        protected virtual void Awake()
        {
            _idHolder = GetComponent<IdHolder>();
            var seeker = GetComponent<Seeker>();
            var thisRigidbody = GetComponent<Rigidbody>();
            _enemyMovement = new EnemyMovement(this, EnemySettings.MovementSettings,
                EnemySettings.TargetPathfinderSettingsSection, seeker, thisRigidbody);
            _targetSelector = new EnemyTargetSelector();
            _targetTriggers.ForEach(trigger => _targetSelector.AddTrigger(trigger));

            _itemsNeedDisabling = new List<IDisableable>
            {
                _enemyMovement,
                Character,
                _targetSelector
            };
            
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
            var dropDirection = _targetSelector.CurrentTarget == null
                ? cashedTransform.forward
                : (_targetSelector.CurrentTarget.MainTransform.position - cashedTransform.position).normalized;
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