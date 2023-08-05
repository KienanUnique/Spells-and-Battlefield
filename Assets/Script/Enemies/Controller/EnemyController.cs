using System;
using System.Collections;
using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Animation_Data;
using Common.Event_Invoker_For_Action_Animations;
using Common.Mechanic_Effects;
using Common.Mechanic_Effects.Continuous_Effect;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Enemies.Character;
using Enemies.Look;
using Enemies.Look_Point_Calculator;
using Enemies.Movement;
using Enemies.Movement.Enemy_Data_For_Moving;
using Enemies.Setup;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Visual;
using Interfaces;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Factory;
using Settings.Enemies;
using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Enemies.Controller
{
    public sealed class EnemyController : InitializableMonoBehaviourBase, IEnemy, IEnemyStateMachineControllable,
        ICoroutineStarter, IInitializableEnemyController
    {
        private const bool NeedCreatedItemsFallDown = true;
        private IEnemyMovement _movement;
        private IEnemyLook _look;
        private IEnemyStateMachineAI _enemyStateMachineAI;
        private IPickableItemDataForCreating _itemToDrop;
        private IIdHolder _idHolder;
        private GeneralEnemySettings _generalEnemySettings;
        private IPickableItemsFactory _itemsFactory;
        private IEnemyTargetFromTriggersSelector _targetFromTriggersSelector;
        private IEventInvokerForActionAnimations _eventInvokerForAnimations;
        private IEnemyVisual _visual;
        private IEnemyCharacter _character;

        public void Initialize(IEnemyBaseSetupData setupData)
        {
            _enemyStateMachineAI = setupData.SetStateMachineAI;
            _itemToDrop = setupData.SetItemToDrop;
            _movement = setupData.SetMovement;
            _idHolder = setupData.SetIdHolder;
            _generalEnemySettings = setupData.SetGeneralEnemySettings;
            _itemsFactory = setupData.SetPickableItemsFactory;
            _targetFromTriggersSelector = setupData.SetTargetFromTriggersSelector;
            _look = setupData.SetLook;
            _eventInvokerForAnimations = setupData.SetEventInvokerForAnimations;
            _visual = setupData.SetVisual;
            _character = setupData.SetCharacter;

            SetItemsNeedDisabling(setupData.SetItemsNeedDisabling);
            SetInitializedStatus();
        }

        public event Action ActionAnimationKeyMomentTrigger;
        public event Action ActionAnimationStart;
        public event Action ActionAnimationEnd;
        public event Action<float> HitPointsCountChanged;
        public event Action<CharacterState> CharacterStateChanged;

        public float HitPointCountRatio => _character.HitPointCountRatio;
        public int Id => _idHolder.Id;
        public Vector3 CurrentPosition => _movement.CurrentPosition;
        public IReadonlyRigidbody ReadonlyRigidbody => _movement.ReadonlyRigidbody;
        public IReadonlyTransform ThisPositionReferencePointForLook => _look.ThisPositionReferencePointForLook;

        public void StartKeepingTransformOnDistance(IReadonlyTransform target, IEnemyDataForMoving dataForMoving)
        {
            _movement.StartKeepingTransformOnDistance(target, dataForMoving);
        }

        public void StopMoving()
        {
            _movement.StopMoving();
        }

        public CharacterState CurrentCharacterState => _character.CurrentCharacterState;
        public IEnemyTargetFromTriggersSelector TargetFromTriggersSelector => _targetFromTriggersSelector;

        public bool Equals(IIdHolder other)
        {
            return _idHolder.Equals(other);
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _movement.AddForce(force, mode);
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

        public void ApplyEffectsToTargets(IReadOnlyCollection<IEnemyTarget> targets,
            IReadOnlyCollection<IMechanicEffect> mechanicEffects)
        {
            _character.ApplyEffectsToTargets(targets, mechanicEffects);
        }

        public void MultiplySpeedRatioBy(float speedRatio)
        {
            _movement.MultiplySpeedRatioBy(speedRatio);
        }

        public void DivideSpeedRatioBy(float speedRatio)
        {
            _movement.DivideSpeedRatioBy(speedRatio);
        }

        public void StickToPlatform(Transform platformTransform)
        {
            _movement.StickToPlatform(platformTransform);
        }

        public void UnstickFromPlatform()
        {
            _movement.UnstickFromPlatform();
        }

        public void PlayActionAnimation(IAnimationData animationData)
        {
            _visual.PlayActionAnimation(animationData);
        }

        public void ChangeThisPositionReferencePointTransform(IReadonlyTransform newReferenceTransform)
        {
            _look.ChangeThisPositionReferencePointTransform(newReferenceTransform);
        }

        protected override void SubscribeOnEvents()
        {
            InitializationStatusChanged += OnInitializationStatusChanged;
            _character.CharacterStateChanged += OnCharacterStateChanged;
            _character.HitPointsCountChanged += OnHitPointsCountChanged;
            _movement.MovingStateChanged += _visual.UpdateMovingData;
            _eventInvokerForAnimations.ActionAnimationEnd += OnActionAnimationEnd;
            _eventInvokerForAnimations.ActionAnimationStart += OnActionAnimationStart;
            _eventInvokerForAnimations.ActionAnimationKeyMomentTrigger += OnActionAnimationKeyMomentTrigger;
            _enemyStateMachineAI.NeedChangeLookPointCalculator += OnNeedChangeLookPointCalculator;
        }

        protected override void UnsubscribeFromEvents()
        {
            InitializationStatusChanged -= OnInitializationStatusChanged;
            _character.CharacterStateChanged -= OnCharacterStateChanged;
            _character.HitPointsCountChanged -= OnHitPointsCountChanged;
            _movement.MovingStateChanged -= _visual.UpdateMovingData;
            _eventInvokerForAnimations.ActionAnimationEnd -= OnActionAnimationEnd;
            _eventInvokerForAnimations.ActionAnimationStart -= OnActionAnimationStart;
            _eventInvokerForAnimations.ActionAnimationKeyMomentTrigger -= OnActionAnimationKeyMomentTrigger;
            _enemyStateMachineAI.NeedChangeLookPointCalculator -= OnNeedChangeLookPointCalculator;
        }

        private void OnInitializationStatusChanged(InitializationStatus newStatus)
        {
            switch (newStatus)
            {
                case InitializationStatus.Initialized:
                    _look.StartLooking();
                    _enemyStateMachineAI.StartStateMachine();
                    break;
                case InitializationStatus.NonInitialized:
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }

        private void OnNeedChangeLookPointCalculator(ILookPointCalculator newCalculator)
        {
            _look.SetLookPointCalculator(newCalculator);
        }

        private void OnCharacterStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _look.StopLooking();
                _enemyStateMachineAI.StopStateMachine();
                _movement.DisableMoving();
                _visual.PlayDieAnimation();
                DropSpell();
                StartCoroutine(DestroyAfterDelay());
            }

            CharacterStateChanged?.Invoke(newState);
        }

        private void OnActionAnimationEnd()
        {
            ActionAnimationEnd?.Invoke();
        }

        private void OnActionAnimationStart()
        {
            ActionAnimationStart?.Invoke();
        }

        private void OnActionAnimationKeyMomentTrigger()
        {
            ActionAnimationKeyMomentTrigger?.Invoke();
        }

        private void OnHitPointsCountChanged(float newHitPointsCount) =>
            HitPointsCountChanged?.Invoke(newHitPointsCount);

        private void DropSpell()
        {
            var cashedTransform = transform;
            var dropDirection = _targetFromTriggersSelector.CurrentTarget == null
                ? cashedTransform.forward
                : (_targetFromTriggersSelector.CurrentTarget.MainRigidbody.Position -
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