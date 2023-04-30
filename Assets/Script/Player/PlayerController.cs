using System;
using Game_Managers;
using Interfaces;
using Spells;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerLook))]
    [RequireComponent(typeof(PlayerSpellsManager))]
    [RequireComponent(typeof(PlayerCharacter))]
    [RequireComponent(typeof(IdHolder))]
    public class PlayerController : MonoBehaviour, IPlayer
    {
        [SerializeField] private PlayerVisual _playerVisual;
        [SerializeField] private PlayerCameraEffects _playerCameraEffects;
        private PlayerCharacter _playerCharacter;
        private PlayerSpellsManager _playerSpellsManager;
        private InGameInputManager _inGameInputManager;
        private PlayerMovement _playerMovement;
        private PlayerLook _playerLook;
        private IdHolder _idHolder;

        public event Action DashAiming;
        public event Action Dashed;
        public event Action DashCooldownFinished;
        public event Action<float> DashCooldownTimerTick;
        public event Action<CharacterState> StateChanged;
        public event Action<float> HitPointsCountChanged;

        public float HitPointCountRatio => _playerCharacter.HitPointCountRatio;
        public int Id => _idHolder.Id;
        public Transform MainTransform => _playerMovement.MainTransform;
        public Vector3 CurrentPosition => _playerMovement.CurrentPosition;
        public ValueWithReactionOnChange<CharacterState> CurrentCharacterState => _playerCharacter.CurrentState;

        public void HandleHeal(int countOfHealthPoints)
        {
            _playerCharacter.HandleHeal(countOfHealthPoints);
        }

        public void HandleDamage(int countOfHealthPoints)
        {
            _playerCharacter.HandleDamage(countOfHealthPoints);
        }

        public void ApplyContinuousEffect(IContinuousEffect effect)
        {
            _playerCharacter.ApplyContinuousEffect(effect);
        }

        public void AddSpell(ISpell newSpell)
        {
            _playerSpellsManager.AddSpell(newSpell);
        }

        public int CompareTo(object obj)
        {
            return _idHolder.CompareTo(obj);
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

        private void Awake()
        {
            _playerCharacter = GetComponent<PlayerCharacter>();
            _playerSpellsManager = GetComponent<PlayerSpellsManager>();
            _inGameInputManager = GameController.Instance.InGameInputManager;
            _playerMovement = GetComponent<PlayerMovement>();
            _playerLook = GetComponent<PlayerLook>();
            _idHolder = GetComponent<IdHolder>();
        }

        private void Update()
        {
            _playerVisual.UpdateMovingData(_playerMovement.NormalizedVelocityDirectionXY,
                _playerMovement.RatioOfCurrentVelocityToMaximumVelocity);
        }

        private void OnEnable()
        {
            _inGameInputManager.JumpInputted += _playerMovement.TryJumpInputted;
            _inGameInputManager.StartDashAimingInputted += _playerMovement.TryStartDashAiming;
            _inGameInputManager.DashInputted += OnDashInputted;
            _inGameInputManager.UseSpellInputted += OnUseSpellInputted;
            _inGameInputManager.MoveInputted += _playerMovement.MoveInputted;
            _inGameInputManager.LookInputted += _playerLook.LookInputtedWith;

            _playerVisual.CastSpellAnimationMoment += OnCastSpellAnimationMoment;

            _playerMovement.GroundJump += _playerVisual.PlayGroundJumpAnimation;
            _playerMovement.Fall += _playerVisual.PlayFallAnimation;
            _playerMovement.Land += _playerVisual.PlayLandAnimation;
            _playerMovement.StartWallRunning += OnStartWallRunning;
            _playerMovement.EndWallRunning += OnEndWallRunning;
            _playerMovement.DashAiming += OnDashAiming;
            _playerMovement.Dashed += OnDashed;
            _playerMovement.DashCooldownFinished += OnDashCooldownFinished;
            _playerMovement.DashCooldownTimerTick += OnDashCooldownTimerTick;

            _playerCharacter.StateChanged += OnCharacterStateChanged;
            _playerCharacter.HitPointsCountChanged += OnHitPointsCountChanged;
        }

        private void OnDisable()
        {
            _inGameInputManager.JumpInputted -= _playerMovement.TryJumpInputted;
            _inGameInputManager.StartDashAimingInputted -= _playerMovement.TryStartDashAiming;
            _inGameInputManager.DashInputted -= OnDashInputted;
            _inGameInputManager.UseSpellInputted -= OnUseSpellInputted;
            _inGameInputManager.MoveInputted -= _playerMovement.MoveInputted;
            _inGameInputManager.LookInputted -= _playerLook.LookInputtedWith;

            _playerVisual.CastSpellAnimationMoment -= OnCastSpellAnimationMoment;

            _playerMovement.GroundJump -= _playerVisual.PlayGroundJumpAnimation;
            _playerMovement.Fall -= _playerVisual.PlayFallAnimation;
            _playerMovement.Land -= _playerVisual.PlayLandAnimation;
            _playerMovement.StartWallRunning -= OnStartWallRunning;
            _playerMovement.EndWallRunning -= OnEndWallRunning;
            _playerMovement.DashAiming -= OnDashAiming;
            _playerMovement.Dashed -= OnDashed;
            _playerMovement.DashCooldownFinished -= OnDashCooldownFinished;
            _playerMovement.DashCooldownTimerTick -= OnDashCooldownTimerTick;

            _playerCharacter.StateChanged -= OnCharacterStateChanged;
            _playerCharacter.HitPointsCountChanged -= OnHitPointsCountChanged;
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

        private void OnDashAiming() => DashAiming?.Invoke();

        private void OnStartWallRunning(WallDirection direction)
        {
            _playerCameraEffects.Rotate(direction);
            _playerVisual.PlayLandAnimation();
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
            StateChanged?.Invoke(newState);
            if (newState == CharacterState.Dead)
            {
                _playerVisual.PlayDieAnimation();
            }
        }

        private void OnUseSpellInputted()
        {
            if (_playerSpellsManager.IsSpellSelected)
            {
                _playerVisual.PlayUseSpellAnimation(_playerSpellsManager.SelectedSpellHandsAnimatorController);
            }
        }

        private void OnCastSpellAnimationMoment()
        {
            if (_playerSpellsManager.IsSpellSelected)
            {
                _playerSpellsManager.UseSelectedSpell(_playerLook.CameraRotation);
            }
        }
    }
}