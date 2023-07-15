using System;
using System.Collections;
using System.Collections.ObjectModel;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Collection_With_Reaction_On_Change;
using Common.Readonly_Transform;
using Interfaces;
using Player.Camera_Effects;
using Player.Character;
using Player.Event_Invoker_For_Animations;
using Player.Look;
using Player.Movement;
using Player.Setup;
using Player.Spell_Manager;
using Player.Visual;
using Spells.Continuous_Effect;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell;
using Spells.Spell.Interfaces;
using Systems.Input_Manager;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerControllerSetup))]
    public class PlayerController : InitializableMonoBehaviourBase, IPlayer, ICoroutineStarter,
        IInitializablePlayerController
    {
        private IIdHolder _idHolder;
        private IPlayerLook _playerLook;
        private IPlayerMovement _playerMovement;
        private IPlayerInput _playerInput;
        private IPlayerSpellsManager _playerSpellsManager;
        private IPlayerCharacter _playerCharacter;
        private IPlayerVisual _playerVisual;
        private IPlayerCameraEffects _playerCameraEffects;
        private IPlayerEventInvokerForAnimations _playerEventInvokerForAnimations;

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
            _playerEventInvokerForAnimations = setupData.SetPlayerEventInvokerForAnimations;

            SetItemsNeedDisabling(setupData.SetItemsNeedDisabling);
            SetInitializedStatus();
        }

        public event Action DashCooldownFinished;
        public event Action<float> DashCooldownTimerTick;
        public event Action<CharacterState> CharacterStateChanged;
        public event Action<float> HitPointsCountChanged;
        public event Action Dashed;
        public event Action DashAiming;
        public event Action<ISpellType> TryingToUseEmptySpellTypeGroup;
        public event Action<ISpellType> SelectedSpellTypeChanged;

        public float HitPointCountRatio => _playerCharacter.HitPointCountRatio;
        public int Id => _idHolder.Id;
        public IReadonlyTransform MainTransform => _playerMovement.MainTransform;
        public Vector3 CurrentPosition => _playerMovement.CurrentPosition;
        public CharacterState CurrentCharacterState => _playerCharacter.CurrentCharacterState;
        public ISpellType SelectedType => _playerSpellsManager.SelectedType;

        public ReadOnlyDictionary<ISpellType, IReadonlyListWithReactionOnChange<ISpell>> Spells =>
            _playerSpellsManager.Spells;

        public void InteractAsSpellType(ISpellType spellType)
        {
        }

        public void HandleHeal(int countOfHealthPoints)
        {
            _playerCharacter.HandleHeal(countOfHealthPoints);
        }

        public void HandleDamage(int countOfHealthPoints)
        {
            _playerCharacter.HandleDamage(countOfHealthPoints);
        }

        public void ApplyContinuousEffect(IAppliedContinuousEffect effect)
        {
            _playerCharacter.ApplyContinuousEffect(effect);
        }

        public void AddSpell(ISpell newSpell)
        {
            _playerSpellsManager.AddSpell(newSpell);
        }

        public bool Equals(IIdHolder other)
        {
            return _idHolder.Equals(other);
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _playerMovement.AddForce(force, mode);
        }

        public void MultiplySpeedRatioBy(float speedRatio)
        {
            _playerMovement.MultiplySpeedRatioBy(speedRatio);
        }

        public void DivideSpeedRatioBy(float speedRatio)
        {
            _playerMovement.DivideSpeedRatioBy(speedRatio);
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

            _playerEventInvokerForAnimations.CastSpellAnimationMoment += OnCastSpellEventInvokerForAnimationMoment;

            _playerMovement.GroundJump += _playerVisual.PlayGroundJumpAnimation;
            _playerMovement.Fall += _playerVisual.PlayFallAnimation;
            _playerMovement.Land += _playerVisual.PlayLandAnimation;
            _playerMovement.StartWallRunning += OnStartWallRunning;
            _playerMovement.WallRunningDirectionChanged += OnWallRunningDirectionChanged;
            _playerMovement.EndWallRunning += OnEndWallRunning;
            _playerMovement.DashAiming += OnDashAiming;
            _playerMovement.Dashed += OnDashed;
            _playerMovement.DashCooldownFinished += OnDashCooldownFinished;
            _playerMovement.DashCooldownTimerTick += OnDashCooldownTimerTick;

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

            _playerEventInvokerForAnimations.CastSpellAnimationMoment -= OnCastSpellEventInvokerForAnimationMoment;

            _playerMovement.GroundJump -= _playerVisual.PlayGroundJumpAnimation;
            _playerMovement.Fall -= _playerVisual.PlayFallAnimation;
            _playerMovement.Land -= _playerVisual.PlayLandAnimation;
            _playerMovement.StartWallRunning -= OnStartWallRunning;
            _playerMovement.WallRunningDirectionChanged -= OnWallRunningDirectionChanged;
            _playerMovement.EndWallRunning -= OnEndWallRunning;
            _playerMovement.DashAiming -= OnDashAiming;
            _playerMovement.Dashed -= OnDashed;
            _playerMovement.DashCooldownFinished -= OnDashCooldownFinished;
            _playerMovement.DashCooldownTimerTick -= OnDashCooldownTimerTick;

            _playerCharacter.CharacterStateChanged -= OnCharacterStateChanged;
            _playerCharacter.HitPointsCountChanged -= OnHitPointsCountChanged;

            _playerSpellsManager.NeedPlaySpellAnimation -= OnNeedPlaySpellAnimation;
            _playerSpellsManager.TryingToUseEmptySpellTypeGroup -= OnTryingToUseEmptySpellCanNotBeUsed;
            _playerSpellsManager.SelectedSpellTypeChanged -= OnSelectedSpellTypeChanged;
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

        private void OnHitPointsCountChanged(float newHitPointsCount) =>
            HitPointsCountChanged?.Invoke(newHitPointsCount);

        private void OnDashCooldownTimerTick(float cooldownRatio) => DashCooldownTimerTick?.Invoke(cooldownRatio);
        private void OnDashCooldownFinished() => DashCooldownFinished?.Invoke();

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

        private void OnNeedPlaySpellAnimation(ISpellAnimationInformation spellAnimationInformation)
        {
            _playerVisual.PlayUseSpellAnimation(spellAnimationInformation.CastAnimationAnimatorOverrideController);
        }

        private void OnCastSpellEventInvokerForAnimationMoment()
        {
            _playerSpellsManager.CreateSelectedSpell(_playerLook.CameraRotation);
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