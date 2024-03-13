using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Character.Hit_Points_Character_Change_Information;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Animation_Data;
using Common.Animation_Data.Continuous_Action;
using Common.Collection_With_Reaction_On_Change;
using Common.Id_Holder;
using Common.Interfaces;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Common.Mechanic_Effects.Continuous_Effect;
using Common.Mechanic_Effects.Source;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Factions;
using Player.Animator_Status_Checker;
using Player.Camera_Effects;
using Player.Character;
using Player.Look;
using Player.Movement;
using Player.Press_Key_Interactor;
using Player.Setup;
using Player.Spell_Manager;
using Player.Visual;
using Player.Visual.Hook_Trail;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using Systems.Input_Manager.Concrete_Types.In_Game;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerControllerSetup))]
    public class PlayerController : InitializableMonoBehaviourBase,
        IPlayer,
        ICoroutineStarter,
        IInitializablePlayerController
    {
        private IIdHolder _idHolder;
        private IPlayerCameraEffects _cameraEffects;
        private IPlayerCharacter _character;
        private IPlayerInput _input;
        private IPlayerLook _look;
        private IPlayerMovement _movement;
        private IPlayerSpellsManager _spellsManager;
        private IPlayerVisual _visual;
        private IPlayerAnimatorStatusChecker _animatorStatusChecker;
        private IHookTrailVisual _hookTrailVisual;
        private IPressKeyInteractor _pressKeyInteractor;

        public void Initialize(IPlayerControllerSetupData setupData)
        {
            _idHolder = setupData.SetIDHolder;
            _look = setupData.SetPlayerLook;
            _movement = setupData.SetPlayerMovement;
            _input = setupData.SetPlayerInput;
            _spellsManager = setupData.SetPlayerSpellsManager;
            _character = setupData.SetPlayerCharacter;
            _visual = setupData.SetPlayerVisual;
            _cameraEffects = setupData.SetPlayerCameraEffects;
            CameraTransform = setupData.SetCameraTransform;
            UpperPointForSummonedEnemiesPositionCalculating =
                setupData.SetUpperPointForSummonedEnemiesPositionCalculating;
            InformationForSummon = setupData.SetInformationForSummon;
            ToolsForSummon = setupData.SetToolsForSummon;
            _animatorStatusChecker = setupData.SetAnimatorStatusChecker;
            _hookTrailVisual = setupData.SetHookTrailVisual;
            _pressKeyInteractor = setupData.SetPressKeyInteractor;

            SetItemsNeedDisabling(setupData.SetItemsNeedDisabling);
            SetInitializedStatus();
        }

        public event Action<CharacterState> CharacterStateChanged;
        public event Action<IHitPointsCharacterChangeInformation> HitPointsCountChanged;
        public event Action<IAppliedContinuousEffectInformation> ContinuousEffectAdded;
        public event Action CanInteractNow;
        public event Action CanNotInteractNow;
        public event Action<float> DashCooldownRatioChanged;
        public event Action Dashed;
        public event Action DashAiming;
        public event Action DashAimingCanceled;
        public event Action ContinuousSpellStarted;
        public event Action ContinuousSpellFinished;
        public event Action<ISpellType> TryingToUseEmptySpellTypeGroup;
        public event Action<ISpellType> SelectedSpellTypeChanged;
        public event Action<IFaction> FactionChanged;
        public IReadonlyTransform UpperPointForSummonedEnemiesPositionCalculating { get; private set; }
        public IInformationForSummon InformationForSummon { get; private set; }
        public IToolsForSummon ToolsForSummon { get; private set; }
        public float HitPointCountRatio => _character.HitPointCountRatio;

        public IReadOnlyList<IAppliedContinuousEffectInformation> CurrentContinuousEffects =>
            _character.CurrentContinuousEffects;

        public CharacterState CurrentCharacterState => _character.CurrentCharacterState;
        public IFaction Faction => _character.Faction;
        public IReadonlyTransform PointForAiming => UpperPointForSummonedEnemiesPositionCalculating;
        public int Id => _idHolder.Id;
        public IReadonlyRigidbody MainRigidbody => _movement.MainRigidbody;
        public bool CanInteract => _pressKeyInteractor.CanInteract;
        public float CurrentDashCooldownRatio => _movement.CurrentDashCooldownRatio;
        public IReadonlyTransform CameraTransform { get; private set; }
        public float ContinuousSpellRatioOfCompletion => _spellsManager.ContinuousSpellRatioOfCompletion;
        public ISpellType SelectedSpellType => _spellsManager.SelectedSpellType;

        public ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>> Spells =>
            _spellsManager.Spells;

        public IReadonlyTransform MainTransform => MainRigidbody;
        public Vector3 CurrentPosition => _movement.CurrentPosition;

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

        public void AddSpell(ISpell newSpell)
        {
            _spellsManager.AddSpell(newSpell);
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

            _input.JumpInputted += _movement.TryJumpInputted;
            _input.StartDashAimingInputted += _movement.TryStartDashAiming;
            _input.DashInputted += OnDashInputted;
            _input.StartUsingSpellInputted += _spellsManager.StartCasting;
            _input.StopUsingSpellInputted += _spellsManager.StopCasting;
            _input.MoveInputted += _movement.MoveInputted;
            _input.LookInputted += _look.LookInputtedWith;
            _input.UseHookInputted += OnUseHookInputted;
            _input.InteractInputted += _pressKeyInteractor.TryInteract;

            _input.SelectSpellTypeWithIndex += _spellsManager.SelectSpellTypeWithIndex;
            _input.SelectNextSpellType += _spellsManager.SelectNextSpellType;
            _input.SelectPreviousSpellType += _spellsManager.SelectPreviousSpellType;

            _movement.GroundJump += _visual.PlayGroundJumpAnimation;
            _movement.Fall += _visual.PlayFallAnimation;
            _movement.Land += _visual.PlayLandAnimation;
            _movement.StartWallRunning += OnStartWallRunning;
            _movement.WallRunningDirectionChanged += OnWallRunningDirectionChanged;
            _movement.EndWallRunning += OnEndWallRunning;
            _movement.DashAiming += OnDashAiming;
            _movement.DashAimingCanceled += OnDashAimingCanceled;
            _movement.Dashed += OnDashed;
            _movement.DashCooldownRatioChanged += OnDashCooldownRatioChanged;
            _movement.OverSpeedValueChanged += _cameraEffects.UpdateOverSpeedValue;
            _movement.HookingStarted += OnHookingStarted;
            _movement.HookingEnded += OnHookingEnded;

            _character.CharacterStateChanged += OnCharacterStateChanged;
            _character.HitPointsCountChanged += OnHitPointsCountChanged;
            _character.ContinuousEffectAdded += OnContinuousEffectAdded;

            _spellsManager.NeedPlaySingleActionAnimation += OnNeedPlayActionAnimation;
            _spellsManager.NeedPlayContinuousActionAnimation += OnNeedPlayActionAnimation;
            _spellsManager.NeedCancelActionAnimations += OnNeedPlayContinuousActionAnimation;
            _spellsManager.TryingToUseEmptySpellTypeGroup += OnTryingToUseEmptySpellCanNotBeUsed;
            _spellsManager.SelectedSpellTypeChanged += OnSelectedSpellTypeChanged;
            _spellsManager.ContinuousSpellFinished += OnContinuousSpellFinished;
            _spellsManager.ContinuousSpellStarted += OnContinuousSpellStarted;

            _hookTrailVisual.TrailArrivedToHookPoint += OnTrailArrivedToHookPoint;
            _animatorStatusChecker.HookKeyMomentTrigger += OnHookKeyMomentTrigger;

            _pressKeyInteractor.CanInteractNow += OnCanInteractNow;
            _pressKeyInteractor.CanNotInteractNow += OnCanNotInteractNow;
        }

        protected override void UnsubscribeFromEvents()
        {
            InitializationStatusChanged -= OnInitializationStatusChanged;

            _input.JumpInputted -= _movement.TryJumpInputted;
            _input.StartDashAimingInputted -= _movement.TryStartDashAiming;
            _input.DashInputted -= OnDashInputted;
            _input.StartUsingSpellInputted -= _spellsManager.StartCasting;
            _input.StopUsingSpellInputted -= _spellsManager.StopCasting;
            _input.MoveInputted -= _movement.MoveInputted;
            _input.LookInputted -= _look.LookInputtedWith;
            _input.UseHookInputted -= OnUseHookInputted;
            _input.InteractInputted -= _pressKeyInteractor.TryInteract;

            _input.SelectSpellTypeWithIndex -= _spellsManager.SelectSpellTypeWithIndex;
            _input.SelectNextSpellType -= _spellsManager.SelectNextSpellType;
            _input.SelectPreviousSpellType -= _spellsManager.SelectPreviousSpellType;

            _movement.GroundJump -= _visual.PlayGroundJumpAnimation;
            _movement.Fall -= _visual.PlayFallAnimation;
            _movement.Land -= _visual.PlayLandAnimation;
            _movement.StartWallRunning -= OnStartWallRunning;
            _movement.WallRunningDirectionChanged -= OnWallRunningDirectionChanged;
            _movement.EndWallRunning -= OnEndWallRunning;
            _movement.DashAiming -= OnDashAiming;
            _movement.DashAimingCanceled -= OnDashAimingCanceled;
            _movement.Dashed -= OnDashed;
            _movement.DashCooldownRatioChanged -= OnDashCooldownRatioChanged;
            _movement.OverSpeedValueChanged -= _cameraEffects.UpdateOverSpeedValue;
            _movement.HookingStarted -= OnHookingStarted;
            _movement.HookingEnded -= OnHookingEnded;

            _character.CharacterStateChanged -= OnCharacterStateChanged;
            _character.HitPointsCountChanged -= OnHitPointsCountChanged;
            _character.ContinuousEffectAdded -= OnContinuousEffectAdded;

            _spellsManager.NeedPlaySingleActionAnimation -= OnNeedPlayActionAnimation;
            _spellsManager.NeedPlayContinuousActionAnimation -= OnNeedPlayActionAnimation;
            _spellsManager.NeedCancelActionAnimations -= OnNeedPlayContinuousActionAnimation;
            _spellsManager.TryingToUseEmptySpellTypeGroup -= OnTryingToUseEmptySpellCanNotBeUsed;
            _spellsManager.SelectedSpellTypeChanged -= OnSelectedSpellTypeChanged;
            _spellsManager.ContinuousSpellFinished -= OnContinuousSpellFinished;
            _spellsManager.ContinuousSpellStarted -= OnContinuousSpellStarted;

            _hookTrailVisual.TrailArrivedToHookPoint -= OnTrailArrivedToHookPoint;
            _animatorStatusChecker.HookKeyMomentTrigger -= OnHookKeyMomentTrigger;

            _pressKeyInteractor.CanInteractNow -= OnCanInteractNow;
            _pressKeyInteractor.CanNotInteractNow -= OnCanNotInteractNow;
        }

        private void OnCanNotInteractNow()
        {
            CanNotInteractNow?.Invoke();
        }

        private void OnCanInteractNow()
        {
            CanInteractNow?.Invoke();
        }

        private void OnHookKeyMomentTrigger()
        {
            _hookTrailVisual.MoveTrailToPoint(_movement.HookPoint);
        }

        private void OnUseHookInputted()
        {
            if (_animatorStatusChecker.IsReadyToPlayActionAnimations)
            {
                _movement.TryStartHook();
            }
        }

        private void OnHookingStarted()
        {
            _animatorStatusChecker.HandleHookStart();
            _visual.StartPlayingHookAnimation();
            _look.StartLookingAtPoint(_movement.HookPoint);
        }

        private void OnHookingEnded()
        {
            _hookTrailVisual.Disappear();
            _animatorStatusChecker.HandleHookEnd();
            _visual.StopPlayingHookAnimation();
            _look.StopLookingAtPoint();
        }

        private void OnNeedPlayContinuousActionAnimation()
        {
            _animatorStatusChecker.HandleActionAnimationCancel();
            _visual.CancelActionAnimation();
        }

        private void OnNeedPlayActionAnimation(IAnimationData animationData)
        {
            _animatorStatusChecker.HandleActionAnimationPlay();
            _visual.PlayActionAnimation(animationData);
        }

        private void OnNeedPlayActionAnimation(IContinuousActionAnimationData animationData)
        {
            _animatorStatusChecker.HandleActionAnimationPlay();
            _visual.PlayActionAnimation(animationData);
        }

        private void OnContinuousSpellStarted()
        {
            ContinuousSpellStarted?.Invoke();
        }

        private void OnContinuousSpellFinished()
        {
            ContinuousSpellFinished?.Invoke();
        }

        private void OnContinuousEffectAdded(IAppliedContinuousEffectInformation newEffect)
        {
            ContinuousEffectAdded?.Invoke(newEffect);
        }

        private void OnDashCooldownRatioChanged(float newCooldownRatio)
        {
            DashCooldownRatioChanged?.Invoke(newCooldownRatio);
        }

        private void OnTrailArrivedToHookPoint()
        {
            _visual.PlayHookPushingAnimation();
            _movement.StartPushingTowardsHook();
        }

        private void OnInitializationStatusChanged(InitializableMonoBehaviourStatus newStatus)
        {
            switch (newStatus)
            {
                case InitializableMonoBehaviourStatus.Initialized:
                    _animatorStatusChecker.StartChecking();
                    StartCoroutine(UpdateMovingDataCoroutine());
                    break;
                case InitializableMonoBehaviourStatus.NonInitialized:
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }

        private IEnumerator UpdateMovingDataCoroutine()
        {
            while (true)
            {
                _visual.UpdateMovingData(_movement.NormalizedVelocityDirectionXY,
                    _movement.RatioOfCurrentVelocityToMaximumVelocity);
                yield return null;
            }
        }

        private void OnHitPointsCountChanged(IHitPointsCharacterChangeInformation changeInformation)
        {
            HitPointsCountChanged?.Invoke(changeInformation);
        }

        private void OnDashed()
        {
            Dashed?.Invoke();
            _cameraEffects.PlayIncreaseFieldOfViewAnimation();
        }

        private void OnDashAiming()
        {
            DashAiming?.Invoke();
        }

        private void OnDashAimingCanceled()
        {
            DashAimingCanceled?.Invoke();
        }

        private void OnStartWallRunning(WallDirection direction)
        {
            _cameraEffects.Rotate(direction);
            _visual.PlayLandAnimation();
        }

        private void OnWallRunningDirectionChanged(WallDirection direction)
        {
            _cameraEffects.Rotate(direction);
        }

        private void OnEndWallRunning()
        {
            _cameraEffects.ResetRotation();
            _visual.PlayFallAnimation();
        }

        private void OnDashInputted()
        {
            _movement.TryDash(_look.LookDirection);
        }

        private void OnCharacterStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _animatorStatusChecker.StopChecking();
                _movement.DisableMoving();
                _visual.PlayDieAnimation();
            }

            CharacterStateChanged?.Invoke(newState);
        }

        private void OnSelectedSpellTypeChanged(ISpellType spellType)
        {
            SelectedSpellTypeChanged?.Invoke(spellType);
        }

        private void OnTryingToUseEmptySpellCanNotBeUsed(ISpellType spellType)
        {
            TryingToUseEmptySpellTypeGroup?.Invoke(spellType);
        }
    }
}