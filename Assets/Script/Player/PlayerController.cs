using Interfaces;
using Spells;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerInputManager))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerLook))]
    [RequireComponent(typeof(PlayerSpellsManager))]
    [RequireComponent(typeof(PlayerCharacter))]
    [RequireComponent(typeof(IdHolder))]
    public class PlayerController : MonoBehaviour, IPlayer
    {
        public int Id => _idHolder.Id;
        public Transform MainTransform => _playerMovement.LocalTransform;
        public Vector3 CurrentPosition => _playerMovement.CurrentPosition;
        [SerializeField] private PlayerVisual _playerVisual;
        private PlayerCharacter _playerCharacter;
        private PlayerSpellsManager _playerSpellsManager;
        private PlayerInputManager _playerInputManager;
        private PlayerMovement _playerMovement;
        private PlayerLook _playerLook;
        private IdHolder _idHolder;

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

        private void Awake()
        {
            _playerCharacter = GetComponent<PlayerCharacter>();
            _playerSpellsManager = GetComponent<PlayerSpellsManager>();
            _playerInputManager = GetComponent<PlayerInputManager>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerLook = GetComponent<PlayerLook>();
            _idHolder = GetComponent<IdHolder>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            _playerVisual.UpdateMovingData(_playerMovement.NormalizedVelocityDirectionXY,
                _playerMovement.RatioOfCurrentVelocityToMaximumVelocity);
        }

        private void OnEnable()
        {
            _playerInputManager.JumpEvent += _playerMovement.Jump;
            _playerInputManager.UseSpellEvent += StartUseSelectedSpell;
            _playerInputManager.MoveInputEvent += _playerMovement.Move;
            _playerInputManager.MouseLookEvent += _playerLook.LookWithMouse;
            _playerInputManager.WalkStartEvent += _playerMovement.StartWalking;
            _playerInputManager.WalkCancelEvent += _playerMovement.StartRunning;
            _playerVisual.UseSpellAnimationMomentStartEvent += UseSelectedSpell;
            _playerMovement.JumpEvent += _playerVisual.PlayJumpAnimation;
            _playerMovement.FallEvent += _playerVisual.PlayFallAnimation;
            _playerMovement.LandEvent += _playerVisual.PlayLandAnimation;
        }

        private void OnDisable()
        {
            _playerInputManager.JumpEvent -= _playerMovement.Jump;
            _playerInputManager.UseSpellEvent -= StartUseSelectedSpell;
            _playerInputManager.MoveInputEvent -= _playerMovement.Move;
            _playerInputManager.MouseLookEvent -= _playerLook.LookWithMouse;
            _playerInputManager.WalkStartEvent -= _playerMovement.StartWalking;
            _playerInputManager.WalkCancelEvent -= _playerMovement.StartRunning;
            _playerVisual.UseSpellAnimationMomentStartEvent -= UseSelectedSpell;
            _playerMovement.JumpEvent -= _playerVisual.PlayJumpAnimation;
            _playerMovement.FallEvent -= _playerVisual.PlayFallAnimation;
            _playerMovement.LandEvent -= _playerVisual.PlayLandAnimation;
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