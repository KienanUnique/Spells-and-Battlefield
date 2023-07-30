using System;
using System.Collections;
using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Animation_Data;
using Common.Mechanic_Effects;
using Common.Mechanic_Effects.Continuous_Effect;
using Common.Readonly_Transform;
using Enemies.Character;
using Enemies.Look;
using Enemies.Look_Point_Calculator;
using Enemies.Movement;
using Enemies.Setup;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Visual;
using Enemies.Visual.Event_Invoker_For_Animations;
using Interfaces;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Factory;
using Settings.Enemy;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Enemies.Controller
{
    public sealed class EnemyController : InitializableMonoBehaviourBase, IEnemy, IEnemyStateMachineControllable,
        ICoroutineStarter, IInitializableEnemyController
    {
        private const bool NeedCreatedItemsFallDown = true;
        private IEnemyMovement _enemyMovement;
        private IEnemyLook _enemyLook;
        private IEnemyStateMachineAI _enemyStateMachineAI;
        private IPickableItemDataForCreating _itemToDrop;
        private IIdHolder _idHolder;
        private GeneralEnemySettings _generalEnemySettings;
        private IPickableItemsFactory _itemsFactory;
        private IEnemyTargetFromTriggersSelector _targetFromTriggersSelector;
        private IEnemyEventInvokerForAnimations _eventInvokerForAnimations;
        private IEnemyVisual _visual;
        private IEnemyCharacter _character;

        public void Initialize(IEnemyBaseSetupData setupData)
        {
            _enemyStateMachineAI = setupData.SetStateMachineAI;
            _itemToDrop = setupData.SetItemToDrop;
            _enemyMovement = setupData.SetMovement;
            _idHolder = setupData.SetIdHolder;
            _generalEnemySettings = setupData.SetGeneralEnemySettings;
            _itemsFactory = setupData.SetPickableItemsFactory;
            _targetFromTriggersSelector = setupData.SetTargetFromTriggersSelector;
            _enemyLook = setupData.SetLook;
            _eventInvokerForAnimations = setupData.SetEventInvokerForAnimations;
            _visual = setupData.SetVisual;
            _character = setupData.SetCharacter;

            SetItemsNeedDisabling(setupData.SetItemsNeedDisabling);
            SetInitializedStatus();
        }

        public event Action AnimationUseActionMomentTrigger;
        public event Action<float> HitPointsCountChanged;
        public event Action<CharacterState> CharacterStateChanged;

        public float HitPointCountRatio => _character.HitPointCountRatio;
        public int Id => _idHolder.Id;
        public Vector3 CurrentPosition => _enemyMovement.CurrentPosition;
        public CharacterState CurrentCharacterState => _character.CurrentCharacterState;
        public IEnemyTargetFromTriggersSelector TargetFromTriggersSelector => _targetFromTriggersSelector;

        public bool Equals(IIdHolder other)
        {
            return _idHolder.Equals(other);
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _enemyMovement.AddForce(force, mode);
        }

        public void InteractAsSpellType(ISpellType spellType)
        {
        }

        public void HandleHeal(int countOfHealthPoints)
        {
            _character.HandleHeal(countOfHealthPoints);
        }

        public void HandleDamage(int countOfHealthPoints)
        {
            _character.HandleDamage(countOfHealthPoints);
        }

        public void ApplyContinuousEffect(IAppliedContinuousEffect effect)
        {
            _character.ApplyContinuousEffect(effect);
        }

        public void StartFollowingObject(IReadonlyTransform target) => _enemyMovement.StartFollowingPosition(target);

        public void StopFollowingObject() => _enemyMovement.StopMovingToTarget();

        public void StartPlayingActionAnimation(IAnimationData animationData)
        {
            Debug.Log("StartPlayingActionAnimation");
            _visual.StartPlayingActionAnimation(animationData);
        }

        public void StopPlayingActionAnimation()
        {
            _visual.StopPlayingActionAnimation();
        }

        public void ApplyEffectsToTargets(IReadOnlyCollection<IEnemyTarget> targets,
            IReadOnlyCollection<IMechanicEffect> mechanicEffects)
        {
            _character.ApplyEffectsToTargets(targets, mechanicEffects);
        }

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
            _character.CharacterStateChanged += OnCharacterStateChanged;
            _character.HitPointsCountChanged += OnHitPointsCountChanged;
            _enemyMovement.MovingStateChanged += _visual.UpdateMovingData;
            _eventInvokerForAnimations.AnimationUseActionMomentTrigger += OnAnimationUseActionMomentTrigger;
            _enemyStateMachineAI.NeedChangeLookPointCalculator += OnNeedChangeLookPointCalculator;
        }

        protected override void UnsubscribeFromEvents()
        {
            InitializationStatusChanged -= OnInitializationStatusChanged;
            _character.CharacterStateChanged -= OnCharacterStateChanged;
            _character.HitPointsCountChanged -= OnHitPointsCountChanged;
            _enemyMovement.MovingStateChanged -= _visual.UpdateMovingData;
            _eventInvokerForAnimations.AnimationUseActionMomentTrigger -= OnAnimationUseActionMomentTrigger;
            _enemyStateMachineAI.NeedChangeLookPointCalculator -= OnNeedChangeLookPointCalculator;
        }

        private void OnInitializationStatusChanged(InitializationStatus newStatus)
        {
            switch (newStatus)
            {
                case InitializationStatus.Initialized:
                    _enemyLook.StartLooking();
                    _enemyStateMachineAI.StartStateMachine();
                    break;
                case InitializationStatus.NonInitialized:
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }

        private void OnNeedChangeLookPointCalculator(ILookPointCalculator newCalculator)
        {
            _enemyLook.SetLookPointCalculator(newCalculator);
        }

        private void OnAnimationUseActionMomentTrigger()
        {
            Debug.Log("OnAnimationUseActionMomentTrigger");
            AnimationUseActionMomentTrigger?.Invoke();
        }

        private void OnCharacterStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _enemyLook.StopLooking();
                _enemyStateMachineAI.StopStateMachine();
                _enemyMovement.DisableMoving();
                _visual.PlayDieAnimation();
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
                : (((IReadonlyTransform) _targetFromTriggersSelector.CurrentTarget.MainRigidbody).Position -
                   cashedTransform.position)
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