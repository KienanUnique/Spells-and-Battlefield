using System;
using System.Collections;
using System.Collections.ObjectModel;
using Common;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Animation_Data;
using Common.Collection_With_Reaction_On_Change;
using Common.Event_Invoker_For_Action_Animations;
using Common.Mechanic_Effects.Continuous_Effect;
using Common.Readonly_Rigidbody;
using Common.Readonly_Transform;
using Factions;
using Interfaces;
using Player.Camera_Effects;
using Player.Character;
using Player.Look;
using Player.Movement;
using Player.Setup;
using Player.Spell_Manager;
using Player.Visual;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using Systems.Input_Manager;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerControllerSetup))]
    public class PlayerController : InitializableMonoBehaviourBase,
        IPlayer,
        ICoroutineStarter,
        IInitializablePlayerController
    {
        private IEventInvokerForActionAnimations _eventInvokerForAnimations;
        private IIdHolder _idHolder;
        private IPlayerCameraEffects _playerCameraEffects;
        private IPlayerCharacter _playerCharacter;
        private IPlayerInput _playerInput;
        private IPlayerLook _playerLook;
        private IPlayerMovement _playerMovement;
        private IPlayerSpellsManager _playerSpellsManager;
        private IPlayerVisual _playerVisual;
        private IReadonlyTransform _pointForAiming;

        public void Initialize(IPlayerControllerSetupData setupData)
        {
            _idHolder = setupData.SetIDHolder;
            _playerLook = setupData.SetPlayerLook;
            _playerMovement = setupData.SetPlayerMovement;
            _playerInput = setupData.SetPlayerInput;
            _playerSpellsManager = setupData.SetPlayerSpellsManager;
            _playerCharacter = setupData.SetPlayerCharacter;
            _playerVisual = setupData.SetPlayerVisual;
            _playerCameraEffects = setupData.SetPlayerCameraEffects;
            _eventInvokerForAnimations = setupData.SetPlayerEventInvokerForAnimations;
            CameraTransform = setupData.SetCameraTransform;
            _pointForAiming = setupData.SetPointForAiming;
            Faction = setupData.SetFaction;

            SetItemsNeedDisabling(setupData.SetItemsNeedDisabling);
            SetInitializedStatus();
        }

        public event Action<CharacterState> CharacterStateChanged;
        public event ICharacterInformationProvider.OnHitPointsCountChanged HitPointsCountChanged;
        public event Action<float> DashCooldownRatioChanged;
        public event Action Dashed;
        public event Action DashAiming;
        public event Action<ISpellType> TryingToUseEmptySpellTypeGroup;
        public event Action<ISpellType> SelectedSpellTypeChanged;
        public event Action<IEnemyTarget> Destroying;

        public float HitPointCountRatio => _playerCharacter.HitPointCountRatio;
        public CharacterState CurrentCharacterState => _playerCharacter.CurrentCharacterState;
        public IFaction Faction { get; private set; }
        public IReadonlyRigidbody MainRigidbody => _playerMovement.MainRigidbody;
        public IReadonlyTransform PointForAiming => _pointForAiming;
        public int Id => _idHolder.Id;
        public Vector3 CurrentPosition => _playerMovement.CurrentPosition;
        public float CurrentDashCooldownRatio => _playerMovement.CurrentDashCooldownRatio;
        public IReadonlyTransform CameraTransform { get; private set; }
        public ISpellType SelectedType => _playerSpellsManager.SelectedType;

        public ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>> Spells =>
            _playerSpellsManager.Spells;

        public void ApplyContinuousEffect(IAppliedContinuousEffect effect)
        {
            _playerCharacter.ApplyContinuousEffect(effect);
        }

        public void HandleDamage(int countOfHealthPoints)
        {
            _playerCharacter.HandleDamage(countOfHealthPoints);
        }

        public bool Equals(IIdHolder other)
        {
            return _idHolder.Equals(other);
        }

        public void HandleHeal(int countOfHitPoints)
        {
            _playerCharacter.HandleHeal(countOfHitPoints);
        }

        public void MultiplySpeedRatioBy(float speedRatio)
        {
            _playerMovement.MultiplySpeedRatioBy(speedRatio);
        }

        public void DivideSpeedRatioBy(float speedRatio)
        {
            _playerMovement.DivideSpeedRatioBy(speedRatio);
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _playerMovement.AddForce(force, mode);
        }

        public void AddSpell(ISpell newSpell)
        {
            _playerSpellsManager.AddSpell(newSpell);
        }

        public void InteractAsSpellType(ISpellType spellType)
        {
        }

        public void StickToPlatform(Transform platformTransform)
        {
            _playerMovement.StickToPlatform(platformTransform);
        }

        public void UnstickFromPlatform()
        {
            _playerMovement.UnstickFromPlatform();
        }

        protected override void SubscribeOnEvents()
        {
            InitializationStatusChanged += OnInitializationStatusChanged;

            _playerInput.JumpInputted += _playerMovement.TryJumpInputted;
            _playerInput.StartDashAimingInputted += _playerMovement.TryStartDashAiming;
            _playerInput.DashInputted += OnDashInputted;
            _playerInput.UseSpellInputted += OnUseSpellInputted;
            _playerInput.MoveInputted += _playerMovement.MoveInputted;
            _playerInput.LookInputted += _playerLook.LookInputtedWith;
            _playerInput.SelectSpellType += _playerSpellsManager.SelectSpellType;

            _eventInvokerForAnimations.ActionAnimationKeyMomentTrigger += OnCastSpellEventInvokerForAnimationMoment;
            _eventInvokerForAnimations.ActionAnimationEnd += _playerSpellsManager.HandleAnimationEnd;

            _playerMovement.GroundJump += _playerVisual.PlayGroundJumpAnimation;
            _playerMovement.Fall += _playerVisual.PlayFallAnimation;
            _playerMovement.Land += _playerVisual.PlayLandAnimation;
            _playerMovement.StartWallRunning += OnStartWallRunning;
            _playerMovement.WallRunningDirectionChanged += OnWallRunningDirectionChanged;
            _playerMovement.EndWallRunning += OnEndWallRunning;
            _playerMovement.DashAiming += OnDashAiming;
            _playerMovement.Dashed += OnDashed;
            _playerMovement.DashCooldownRatioChanged += OnDashCooldownRatioChanged;

            _playerCharacter.CharacterStateChanged += OnCharacterStateChanged;
            _playerCharacter.HitPointsCountChanged += OnHitPointsCountChanged;

            _playerSpellsManager.NeedPlaySpellAnimation += OnNeedPlaySpellAnimation;
            _playerSpellsManager.TryingToUseEmptySpellTypeGroup += OnTryingToUseEmptySpellCanNotBeUsed;
            _playerSpellsManager.SelectedSpellTypeChanged += OnSelectedSpellTypeChanged;
        }

        protected override void UnsubscribeFromEvents()
        {
            InitializationStatusChanged -= OnInitializationStatusChanged;

            _playerInput.JumpInputted -= _playerMovement.TryJumpInputted;
            _playerInput.StartDashAimingInputted -= _playerMovement.TryStartDashAiming;
            _playerInput.DashInputted -= OnDashInputted;
            _playerInput.UseSpellInputted -= OnUseSpellInputted;
            _playerInput.MoveInputted -= _playerMovement.MoveInputted;
            _playerInput.LookInputted -= _playerLook.LookInputtedWith;
            _playerInput.SelectSpellType -= _playerSpellsManager.SelectSpellType;

            _eventInvokerForAnimations.ActionAnimationKeyMomentTrigger -= OnCastSpellEventInvokerForAnimationMoment;
            _eventInvokerForAnimations.ActionAnimationEnd -= _playerSpellsManager.HandleAnimationEnd;

            _playerMovement.GroundJump -= _playerVisual.PlayGroundJumpAnimation;
            _playerMovement.Fall -= _playerVisual.PlayFallAnimation;
            _playerMovement.Land -= _playerVisual.PlayLandAnimation;
            _playerMovement.StartWallRunning -= OnStartWallRunning;
            _playerMovement.WallRunningDirectionChanged -= OnWallRunningDirectionChanged;
            _playerMovement.EndWallRunning -= OnEndWallRunning;
            _playerMovement.DashAiming -= OnDashAiming;
            _playerMovement.Dashed -= OnDashed;
            _playerMovement.DashCooldownRatioChanged -= OnDashCooldownRatioChanged;

            _playerCharacter.CharacterStateChanged -= OnCharacterStateChanged;
            _playerCharacter.HitPointsCountChanged -= OnHitPointsCountChanged;

            _playerSpellsManager.NeedPlaySpellAnimation -= OnNeedPlaySpellAnimation;
            _playerSpellsManager.TryingToUseEmptySpellTypeGroup -= OnTryingToUseEmptySpellCanNotBeUsed;
            _playerSpellsManager.SelectedSpellTypeChanged -= OnSelectedSpellTypeChanged;
        }

        private void OnDestroy()
        {
            Destroying?.Invoke(this);
        }

        private void OnDashCooldownRatioChanged(float newCooldownRatio)
        {
            DashCooldownRatioChanged?.Invoke(newCooldownRatio);
        }

        private void OnInitializationStatusChanged(InitializationStatus newStatus)
        {
            switch (newStatus)
            {
                case InitializationStatus.Initialized:
                    StartCoroutine(UpdateMovingDataCoroutine());
                    break;
                case InitializationStatus.NonInitialized:
                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }
        }

        private IEnumerator UpdateMovingDataCoroutine()
        {
            while (true)
            {
                _playerVisual.UpdateMovingData(_playerMovement.NormalizedVelocityDirectionXY,
                    _playerMovement.RatioOfCurrentVelocityToMaximumVelocity);
                yield return null;
            }
        }

        private void OnHitPointsCountChanged(int hitPointsLeft, int hitPointsChangeValue,
            TypeOfHitPointsChange typeOfHitPointsChange)
        {
            HitPointsCountChanged?.Invoke(hitPointsLeft, hitPointsChangeValue, typeOfHitPointsChange);
        }

        private void OnDashed()
        {
            Dashed?.Invoke();
            _playerCameraEffects.PlayIncreaseFieldOfViewAnimation();
        }

        private void OnDashAiming()
        {
            DashAiming?.Invoke();
        }

        private void OnStartWallRunning(WallDirection direction)
        {
            _playerCameraEffects.Rotate(direction);
            _playerVisual.PlayLandAnimation();
        }

        private void OnWallRunningDirectionChanged(WallDirection direction)
        {
            _playerCameraEffects.Rotate(direction);
        }

        private void OnEndWallRunning()
        {
            _playerCameraEffects.ResetRotation();
            _playerVisual.PlayFallAnimation();
        }

        private void OnDashInputted()
        {
            _playerMovement.TryDash(_playerLook.CameraForward);
        }

        private void OnCharacterStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _playerVisual.PlayDieAnimation();
            }

            CharacterStateChanged?.Invoke(newState);
        }

        private void OnUseSpellInputted()
        {
            _playerSpellsManager.TryCastSelectedSpell();
        }

        private void OnNeedPlaySpellAnimation(IAnimationData spellAnimationData)
        {
            _playerVisual.PlayUseSpellAnimation(spellAnimationData);
        }

        private void OnCastSpellEventInvokerForAnimationMoment()
        {
            _playerSpellsManager.CreateSelectedSpell(_playerLook.CameraLookPointPosition);
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