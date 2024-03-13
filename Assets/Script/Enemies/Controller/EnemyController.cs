using System;
using System.Collections;
using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Character.Hit_Points_Character_Change_Information;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Animation_Data;
using Common.Animation_Data.Continuous_Action;
using Common.Animator_Status_Controller;
using Common.Id_Holder;
using Common.Interfaces;
using Common.Mechanic_Effects;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Mechanic_Effects.Continuous_Effect;
using Common.Mechanic_Effects.Source;
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
using Spells.Implementations_Interfaces.Implementations;
using UI.Concrete_Scenes.In_Game.Popup_Text.Factory;
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
        private IAnimatorStatusChecker _animatorStatusChecker;
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
            _animatorStatusChecker = setupData.SetAnimatorStatusChecker;
            _visual = setupData.SetVisual;
            _character = setupData.SetCharacter;
            _popupTextHitPointsChangeAppearCenterPoint = setupData.SetPopupTextHitPointsChangeAppearCenterPoint;
            PointForAiming = setupData.SetPointForAiming;
            InformationForSummon = setupData.SetInformationForSummon;
            ToolsForSummon = setupData.SetToolsForSummon;
            UpperPointForSummonedEnemiesPositionCalculating =
                setupData.SetUpperPointForSummonedEnemiesPositionCalculating;

            SetItemsNeedDisabling(setupData.SetItemsNeedDisabling);
            SetInitializedStatus();
        }

        public event Action<IHitPointsCharacterChangeInformation> HitPointsCountChanged;
        public event Action<IAppliedContinuousEffectInformation> ContinuousEffectAdded;
        public event Action<CharacterState> CharacterStateChanged;
        public event Action<IFaction> FactionChanged;
        public IReadonlyTransform UpperPointForSummonedEnemiesPositionCalculating { get; private set; }
        public IInformationForSummon InformationForSummon { get; private set; }
        public IToolsForSummon ToolsForSummon { get; private set; }
        public float HitPointCountRatio => _character.HitPointCountRatio;

        public IReadOnlyList<IAppliedContinuousEffectInformation> CurrentContinuousEffects =>
            _character.CurrentContinuousEffects;

        public CharacterState CurrentCharacterState => _character.CurrentCharacterState;
        public ISummoner Summoner => _character.Summoner;
        public IReadonlyTransform ThisPositionReferencePointForLook => _look.ThisPositionReferencePointForLook;
        public IReadonlyRigidbody ReadonlyRigidbody => _movement.ReadonlyRigidbody;

        public IEnemyTargetFromTriggersSelector TargetFromTriggersSelector { get; private set; }
        public IFaction Faction => _character.Faction;
        public IReadonlyTransform PointForAiming { get; private set; }
        public int Id => _idHolder.Id;

        public IReadonlyTransform MainTransform => MainRigidbody;
        public IReadonlyRigidbody MainRigidbody => _movement.ReadonlyRigidbody;
        public Vector3 CurrentPosition => _movement.CurrentPosition;
        public Vector3 LookPointPosition => _look.LookPointPosition;
        public Vector3 LookDirection => _look.LookDirection;

        public void DieInstantly()
        {
            _character.DieInstantly();
        }

        public void ApplyContinuousEffect(IAppliedContinuousEffect effect)
        {
            _character.ApplyContinuousEffect(effect);
        }

        public void HandleDamage(int countOfHealthPoints, IEffectSourceInformation sourceInformation)
        {
            _character.HandleDamage(countOfHealthPoints, sourceInformation);
        }

        public void PlayActionAnimation(IAnimationData animationData)
        {
            _animatorStatusChecker.HandleActionAnimationPlay();
            _visual.PlayActionAnimation(animationData);
        }

        public void PlayActionAnimation(IContinuousActionAnimationData animationData)
        {
            _animatorStatusChecker.HandleActionAnimationPlay();
            _visual.PlayActionAnimation(animationData);
        }

        public void CancelActionAnimation()
        {
            _animatorStatusChecker.HandleActionAnimationCancel();
            _visual.CancelActionAnimation();
        }

        public void ChangeThisPositionReferencePointTransform(IReadonlyTransform newReferenceTransform)
        {
            _look.ChangeThisPositionReferencePointTransform(newReferenceTransform);
        }

        public void StartKeepingCurrentTargetOnDistance(IEnemyDataForMoving dataForMoving)
        {
            _movement.StartKeepingCurrentTargetOnDistance(dataForMoving);
        }

        public void StartKeepingSummonerOnDistance(IEnemyDataForMoving dataForMoving)
        {
            _movement.StartKeepingSummonerOnDistance(dataForMoving);
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

        public void HandleHeal(int countOfHitPoints, IEffectSourceInformation sourceInformation)
        {
            _character.HandleHeal(countOfHitPoints, sourceInformation);
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
        
        public void RevertFaction()
        {
            _character.RevertFaction();
        }

        public void ResetFactionToDefault()
        {
            _character.ResetFactionToDefault();
        }

        protected override void SubscribeOnEvents()
        {
            InitializationStatusChanged += OnInitializationStatusChanged;
            _character.CharacterStateChanged += OnCharacterStateChanged;
            _character.HitPointsCountChanged += OnHitPointsCountChanged;
            _character.ContinuousEffectAdded += OnContinuousEffectAdded;
            _character.FactionChanged += OnFactionChanged;
            _movement.MovingStateChanged += _visual.UpdateMovingData;
            _enemyStateMachineAI.NeedChangeLookPointCalculator += OnNeedChangeLookPointCalculator;
        }

        protected override void UnsubscribeFromEvents()
        {
            InitializationStatusChanged -= OnInitializationStatusChanged;
            _character.CharacterStateChanged -= OnCharacterStateChanged;
            _character.HitPointsCountChanged -= OnHitPointsCountChanged;
            _character.ContinuousEffectAdded -= OnContinuousEffectAdded;
            _character.FactionChanged -= OnFactionChanged;
            _movement.MovingStateChanged -= _visual.UpdateMovingData;
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
                    _animatorStatusChecker.StartChecking();
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
                _animatorStatusChecker.StopChecking();
                DropSpell();
                StartCoroutine(DestroyAfterDelay());
            }

            CharacterStateChanged?.Invoke(newState);
        }

        private void OnContinuousEffectAdded(IAppliedContinuousEffectInformation newEffect)
        {
            ContinuousEffectAdded?.Invoke(newEffect);
        }

        private void OnHitPointsCountChanged(IHitPointsCharacterChangeInformation changeInformation)
        {
            _popupHitPointsChangeTextFactory.Create(changeInformation.TypeOfHitPointsChange,
                changeInformation.HitPointsChangeValue, _popupTextHitPointsChangeAppearCenterPoint.Position);
            HitPointsCountChanged?.Invoke(changeInformation);
        }
        
        private void OnFactionChanged(IFaction obj)
        {
            FactionChanged?.Invoke(obj);
        }

        private void DropSpell()
        {
            var cashedTransform = transform;
            var dropDirection = TargetFromTriggersSelector.CurrentTarget == null
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