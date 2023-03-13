using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerLook))]
[RequireComponent(typeof(PlayerSpellsManager))]
[RequireComponent(typeof(SpellGameObjectInterface))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _playerCharacter = new PlayerCharacter();
    [SerializeField] private PlayerVisual _playerVisual;
    private PlayerSpellsManager _playerSpellsManager;
    private PlayerInputManager _playerInputManager;
    private PlayerMovement _playerMovement;
    private PlayerLook _playerLook;
    private SpellGameObjectInterface _spellGameObjectInterface;

    private void Awake()
    {
        _playerSpellsManager = GetComponent<PlayerSpellsManager>();
        _playerInputManager = GetComponent<PlayerInputManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerLook = GetComponent<PlayerLook>();
        _spellGameObjectInterface = GetComponent<SpellGameObjectInterface>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _playerVisual.UpdateMovingData(_playerMovement.NormalizedVelocityDirectionXY, _playerMovement.RatioOfCurrentVelocityToMaximumVelocity);
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
        _spellGameObjectInterface.HandleHealEvent += _playerCharacter.HandleHeal;
        _spellGameObjectInterface.HandleDamageEvent += _playerCharacter.HandleDamage;
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
        _spellGameObjectInterface.HandleHealEvent -= _playerCharacter.HandleHeal;
        _spellGameObjectInterface.HandleDamageEvent -= _playerCharacter.HandleDamage;
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
