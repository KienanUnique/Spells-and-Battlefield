using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Disableable;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Readonly_Transform;
using Enemies.Movement;
using Enemies.Setup;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Visual;
using Interfaces;
using Pathfinding;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Factory;
using Settings;
using Settings.Enemy;
using Spells.Continuous_Effect;
using UnityEngine;

namespace Enemies.Controller
{
    [RequireComponent(typeof(IdHolder))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Seeker))]
    public abstract class EnemyControllerBase : InitializableMonoBehaviourBase, IEnemy, IEnemyStateMachineControllable,
        ICoroutineStarter
    {
        private const bool NeedCreatedItemsFallDown = true;
        private IEnemyStateMachineAI _enemyStateMachineAI;
        private IPickableItemDataForCreating _itemToDrop;
        protected IEnemyMovement _enemyMovement;
        private IIdHolder _idHolder;
        private GeneralEnemySettings _generalEnemySettings;
        private IPickableItemsFactory _itemsFactory;
        private IEnemyTargetFromTriggersSelector _targetFromTriggersSelector;

        protected void InitializeBase(IEnemyBaseSetupData setupData)
        {
            _enemyStateMachineAI = setupData.SetEnemyStateMachineAI;
            _itemToDrop = setupData.SetItemToDrop;
            _enemyMovement = setupData.SetEnemyMovement;
            _idHolder = setupData.SetIdHolder;
            _generalEnemySettings = setupData.SetGeneralEnemySettings;
            _itemsFactory = setupData.SetPickableItemsFactory;
            _targetFromTriggersSelector = setupData.SetTargetFromTriggersSelector;

            SetItemsNeedDisabling(setupData.SetItemsNeedDisabling);
            SetInitializedStatus();
        }

        public event Action<float> HitPointsCountChanged;
        public event Action<CharacterState> CharacterStateChanged;

        public float HitPointCountRatio => Character.HitPointCountRatio;
        public int Id => _idHolder.Id;
        public Vector3 CurrentPosition => _enemyMovement.CurrentPosition;
        public CharacterState CurrentCharacterState => Character.CurrentCharacterState;
        protected abstract IEnemyVisualBase EnemyVisual { get; }
        protected abstract IEnemyCharacter Character { get; }

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

        public IEnemyTargetFromTriggersSelector TargetFromTriggersSelector => _targetFromTriggersSelector;
        public void StartMovingToTarget(IReadonlyTransform target) => _enemyMovement.StartFollowingPosition(target);

        public void StopMovingToTarget() => _enemyMovement.StopMovingToTarget();

        public void MultiplySpeedRatioBy(float speedRatio)
        {
            _enemyMovement.MultiplySpeedRatioBy(speedRatio);
        }

        public void DivideSpeedRatioBy(float speedRatio)
        {
            _enemyMovement.DivideSpeedRatioBy(speedRatio);
        }

        public void StickToPlatform(Transform platformTransform)
        {
            _enemyMovement.StickToPlatform(platformTransform);
        }

        public void UnstickFromPlatform()
        {
            _enemyMovement.UnstickFromPlatform();
        }

        protected override void SubscribeOnEvents()
        {
            InitializationStatusChanged += OnInitializationStatusChanged;
            Character.CharacterStateChanged += OnCharacterStateChanged;
            Character.HitPointsCountChanged += OnHitPointsCountChanged;
            _enemyMovement.MovingStateChanged += EnemyVisual.UpdateMovingData;
        }

        protected override void UnsubscribeFromEvents()
        {
            InitializationStatusChanged -= OnInitializationStatusChanged;
            Character.CharacterStateChanged -= OnCharacterStateChanged;
            Character.HitPointsCountChanged -= OnHitPointsCountChanged;
            _enemyMovement.MovingStateChanged -= EnemyVisual.UpdateMovingData;
        }

        private void OnInitializationStatusChanged(InitializationStatus newStatus)
        {
            switch (newStatus)
            {
                case InitializationStatus.Initialized:
                    _enemyStateMachineAI.StartStateMachine(this);
                    break;
                case InitializationStatus.NonInitialized:
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }

        private void OnCharacterStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _enemyStateMachineAI.StopStateMachine();
                _enemyMovement.DisableMoving();
                EnemyVisual.PlayDieAnimation();
                DropSpell();
                StartCoroutine(DestroyAfterDelay());
            }

            CharacterStateChanged?.Invoke(newState);
        }

        private void OnHitPointsCountChanged(float newHitPointsCount) =>
            HitPointsCountChanged?.Invoke(newHitPointsCount);

        private void DropSpell()
        {
            var cashedTransform = transform;
            var dropDirection = _targetFromTriggersSelector.CurrentTarget == null
                ? cashedTransform.forward
                : (_targetFromTriggersSelector.CurrentTarget.MainTransform.Position - cashedTransform.position)
                .normalized;
            var spawnPosition = _generalEnemySettings.SpawnSpellOffset + cashedTransform.position;
            var pickableSpell = _itemsFactory.Create(_itemToDrop, spawnPosition, NeedCreatedItemsFallDown);
            pickableSpell.DropItemTowardsDirection(dropDirection);
        }

        private IEnumerator DestroyAfterDelay()
        {
            yield return new WaitForSeconds(_generalEnemySettings.DelayInSecondsBeforeDestroy);
            Destroy(this.gameObject);
        }
    }
}