using Game_Managers;
using Interfaces;
using Spells;
using UI.Bar;
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
        [SerializeField] private BarController _hpBar;
        private PlayerCharacter _playerCharacter;
        private PlayerSpellsManager _playerSpellsManager;
        private InGameInputManager _inGameInputManager;
        private PlayerMovement _playerMovement;
        private PlayerLook _playerLook;
        private IdHolder _idHolder;

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
            _inGameInputManager.JumpEvent += _playerMovement.TryJump;
            _inGameInputManager.DashAimingEvent += _playerMovement.TryAimForDashing;
            _inGameInputManager.DashEvent += OnDashAiming;
            _inGameInputManager.UseSpellEvent += StartUseSelectedSpell;
            _inGameInputManager.MoveInputEvent += _playerMovement.Move;
            _inGameInputManager.MouseLookEvent += _playerLook.LookWithMouse;
            _playerVisual.UseSpellAnimationMomentStartEvent += UseSelectedSpell;
            _playerMovement.GroundJumpEvent += _playerVisual.PlayGroundJumpAnimation;
            _playerMovement.FallEvent += _playerVisual.PlayFallAnimation;
            _playerMovement.LandEvent += _playerVisual.PlayLandAnimation;
            _playerMovement.StartWallRunningEvent += OnStartWallRunningEvent;
            _playerMovement.EndWallRunningEvent += OnEndWallRunningEvent;
            _playerMovement.DashAiming += TimeController.Instance.SlowDownTimeForDashAiming;
            _playerMovement.DashFinished += TimeController.Instance.RestoreTimeSpeed;
            _playerCharacter.HitPointsCountChanged += OnHitPointsCountChanged;
            _playerCharacter.StateChanged += OnCharacterStateChanged;
        }

        private void OnDisable()
        {
            _inGameInputManager.JumpEvent -= _playerMovement.TryJump;
            _inGameInputManager.DashAimingEvent -= _playerMovement.TryAimForDashing;
            _inGameInputManager.DashEvent -= OnDashAiming;
            _inGameInputManager.UseSpellEvent -= StartUseSelectedSpell;
            _inGameInputManager.MoveInputEvent -= _playerMovement.Move;
            _inGameInputManager.MouseLookEvent -= _playerLook.LookWithMouse;
            _playerVisual.UseSpellAnimationMomentStartEvent -= UseSelectedSpell;
            _playerMovement.GroundJumpEvent -= _playerVisual.PlayGroundJumpAnimation;
            _playerMovement.FallEvent -= _playerVisual.PlayFallAnimation;
            _playerMovement.LandEvent -= _playerVisual.PlayLandAnimation;
            _playerMovement.StartWallRunningEvent -= OnStartWallRunningEvent;
            _playerMovement.EndWallRunningEvent -= OnEndWallRunningEvent;
            _playerCharacter.HitPointsCountChanged -= OnHitPointsCountChanged;
            _playerMovement.DashAiming -= TimeController.Instance.SlowDownTimeForDashAiming;
            _playerMovement.DashFinished -= TimeController.Instance.RestoreTimeSpeed;
            _playerCharacter.StateChanged -= OnCharacterStateChanged;
        }

        private void OnStartWallRunningEvent(WallDirection direction)
        {
            _playerCameraEffects.Rotate(direction);
            _playerVisual.PlayLandAnimation();
        }

        private void OnEndWallRunningEvent()
        {
            _playerCameraEffects.ResetRotation();
            _playerVisual.PlayFallAnimation();
        }

        private void OnDashAiming()
        {
            _playerMovement.TryDash(_playerLook.CameraForward);
        }

        private void OnHitPointsCountChanged(float newHitPointsCount)
        {
            _hpBar.UpdateValue(_playerCharacter.HitPointCountRatio);
        }

        private void OnCharacterStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                _playerVisual.PlayDieAnimation();
            }
        }

        private void StartUseSelectedSpell()
        {
            if (_playerSpellsManager.IsSpellSelected)
            {
                _playerVisual.PlayUseSpellAnimation(_playerSpellsManager.SelectedSpellHandsAnimatorController);
            }
        }

        private void UseSelectedSpell()
        {
            if (_playerSpellsManager.IsSpellSelected)
            {
                _playerSpellsManager.UseSelectedSpell(_playerLook.CameraRotation);
            }
        }
    }
}