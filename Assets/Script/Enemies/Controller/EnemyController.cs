using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Animation_Data;
using Common.Event_Invoker_For_Action_Animations;
using Common.Mechanic_Effects;
using Common.Mechanic_Effects.Continuous_Effect;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Enemies.Character;
using Enemies.General_Settings;
using Enemies.Look;
using Enemies.Look_Point_Calculator;
using Enemies.Loot_Dropper;
using Enemies.Movement;
using Enemies.Movement.Enemy_Data_For_Moving;
using Enemies.Setup.Controller_Setup_Data;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Visual;
using Factions;
using Interfaces;
using Spells.Implementations_Interfaces.Implementations;
using UI.Popup_Text.Factory;
using UnityEngine;

namespace Enemies.Controller
{
    public sealed class EnemyController : InitializableMonoBehaviourBase,
        IEnemy,
        IEnemyStateMachineControllable,
        ICoroutineStarter,
        IInitializableEnemyController
    {
        private IEnemyCharacter _character;
        private IEnemyStateMachineAI _enemyStateMachineAI;
        private IEventInvokerForActionAnimations _eventInvokerForAnimations;
        private IGeneralEnemySettings _generalEnemySettings;
        private IIdHolder _idHolder;
        private ILootDropper _lootDropper;
        private IEnemyLook _look;
        private IEnemyMovement _movement;
        private IPopupHitPointsChangeTextFactory _popupHitPointsChangeTextFactory;
        private IReadonlyTransform _popupTextHitPointsChangeAppearCenterPoint;
        private IEnemyVisual _visual;

        public void Initialize(IEnemyControllerSetupData setupData)
        {
            _enemyStateMachineAI = setupData.SetStateMachineAI;
            _lootDropper = setupData.SetLootDropper;
            _movement = setupData.SetMovement;
            _idHolder = setupData.SetIdHolder;
            _generalEnemySettings = setupData.SetGeneralEnemySettings;
            _popupHitPointsChangeTextFactory = setupData.SetPopupHitPointsChangeTextFactory;
            TargetFromTriggersSelector = setupData.SetTargetFromTriggersSelector;
            _look = setupData.SetLook;
            _eventInvokerForAnimations = setupData.SetEventInvokerForAnimations;
            _visual = setupData.SetVisual;
            _character = setupData.SetCharacter;
            _popupTextHitPointsChangeAppearCenterPoint = setupData.SetPopupTextHitPointsChangeAppearCenterPoint;
            PointForAiming = setupData.SetPointForAiming;
            Faction = setupData.SetFaction;
            InformationForSummon = setupData.SetInformationForSummon;
            ToolsForSummon = setupData.SetToolsForSummon;

            SetItemsNeedDisabling(setupData.SetItemsNeedDisabling);
            SetInitializedStatus();
        }

        public event ICharacterInformationProvider.OnHitPointsCountChanged HitPointsCountChanged;
        public event Action<CharacterState> CharacterStateChanged;

        public event Action ActionAnimationKeyMomentTrigger;
        public event Action ActionAnimationStart;
        public event Action ActionAnimationEnd;

        public float HitPointCountRatio => _character.HitPointCountRatio;

        public void DieInstantly()
        {
            _character.DieInstantly();
        }

        public CharacterState CurrentCharacterState => _character.CurrentCharacterState;
        public Vector3 CurrentLookDirection => _look.CurrentLookDirection;
        public IReadonlyTransform ThisPositionReferencePointForLook => _look.ThisPositionReferencePointForLook;
        public IReadonlyRigidbody ReadonlyRigidbody => _movement.ReadonlyRigidbody;

        public IEnemyTargetFromTriggersSelector TargetFromTriggersSelector { get; private set; }
        public ISummoner Summoner => this;
        public int Id => _idHolder.Id;
        public Vector3 CurrentPosition => _movement.CurrentPosition;
        public IFaction Faction { get; private set; }
        public IReadonlyTransform PointForAiming { get; private set; }
        public IReadonlyRigidbody MainRigidbody => _movement.ReadonlyRigidbody;

        public IReadonlyTransform MainTransform => MainRigidbody;
        public IReadonlyTransform UpperPointForSummonPointCalculating => PointForAiming;
        public IInformationForSummon InformationForSummon { get; private set; }
        public IToolsForSummon ToolsForSummon { get; private set; }

        public void ApplyContinuousEffect(IAppliedContinuousEffect effect)
        {
            _character.ApplyContinuousEffect(effect);
        }

        public void HandleDamage(int countOfHealthPoints)
        {
            _character.HandleDamage(countOfHealthPoints);
        }

        public void PlayActionAnimation(IAnimationData animationData)
        {
            _visual.PlayActionAnimation(animationData);
        }

        public void ChangeThisPositionReferencePointTransform(IReadonlyTransform newReferenceTransform)
        {
            _look.ChangeThisPositionReferencePointTransform(newReferenceTransform);
        }

        public void StartKeepingCurrentTargetOnDistance(IEnemyDataForMoving dataForMoving)
        {
            _movement.StartKeepingCurrentTargetOnDistance(dataForMoving);
        }

        public void StopMoving()
        {
            _movement.StopMoving();
        }

        public void ApplyEffectsToTargets(IReadOnlyCollection<IEnemyTarget> targets,
            IReadOnlyCollection<IMechanicEffect> mechanicEffects)
        {
            _character.ApplyEffectsToTargets(targets, mechanicEffects);
        }

        public bool Equals(IIdHolder other)
        {
            return _idHolder.Equals(other);
        }

        public void HandleHeal(int countOfHitPoints)
        {
            _character.HandleHeal(countOfHitPoints);
        }

        public void MultiplySpeedRatioBy(float speedRatio)
        {
            _movement.MultiplySpeedRatioBy(speedRatio);
        }

        public void DivideSpeedRatioBy(float speedRatio)
        {
            _movement.DivideSpeedRatioBy(speedRatio);
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _movement.AddForce(force, mode);
        }

        public void InteractAsSpellType(ISpellType spellType)
        {
        }

        public void StickToPlatform(Transform platformTransform)
        {
            _movement.StickToPlatform(platformTransform);
        }

        public void UnstickFromPlatform()
        {
            _movement.UnstickFromPlatform();
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

        private void OnInitializationStatusChanged(InitializableMonoBehaviourStatus newStatus)
        {
            switch (newStatus)
            {
                case InitializableMonoBehaviourStatus.Initialized:
                    TargetFromTriggersSelector.StartSelecting();
                    _look.StartLooking();
                    _enemyStateMachineAI.StartStateMachine();
                    break;
                case InitializableMonoBehaviourStatus.NonInitialized:
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
                TargetFromTriggersSelector.StopSelecting();
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

        private void OnHitPointsCountChanged(int hitPointsLeft, int hitPointsChangeValue,
            TypeOfHitPointsChange typeOfHitPointsChange)
        {
            _popupHitPointsChangeTextFactory.Create(typeOfHitPointsChange, hitPointsChangeValue,
                _popupTextHitPointsChangeAppearCenterPoint.Position);
            HitPointsCountChanged?.Invoke(hitPointsLeft, hitPointsChangeValue, typeOfHitPointsChange);
        }

        private void DropSpell()
        {
            Transform cashedTransform = transform;
            Vector3 dropDirection = TargetFromTriggersSelector.CurrentTarget == null
                ? cashedTransform.forward
                : (TargetFromTriggersSelector.CurrentTarget.MainRigidbody.Position - cashedTransform.position)
                .normalized;
            _lootDropper.DropLoot(dropDirection);
        }

        private IEnumerator DestroyAfterDelay()
        {
            yield return new WaitForSeconds(_generalEnemySettings.DelayInSecondsBeforeDestroy);
            Destroy(gameObject);
        }
    }
}