using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerLook))]
public class PlayerController : MonoBehaviour, ICharacter
{
    [SerializeField] private PlayerCharacter _playerCharacter = new PlayerCharacter();
    [SerializeField] private PlayerVisual _playerVisual;
    [SerializeField] private PlayerSpellsManager _playerSpellsManager = new PlayerSpellsManager();
    private PlayerInputManager _playerInputManager;
    private PlayerMovement _playerMovement;
    private PlayerLook _playerLook;

    public void HandleHeal(int countOfHealPoints) => _playerCharacter.HandleHeal(countOfHealPoints);

    public void HandleDamage(int countOfHealPoints) => _playerCharacter.HandleDamage(countOfHealPoints);

    public void HandleVelocityBoost() => Debug.Log($"Player -> HandleVelocityBoost");

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerLook = GetComponent<PlayerLook>();
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
            _playerSpellsManager.UseSelectedSpell(this, _playerMovement.LocalTransform, _playerLook.CameraRotation);
        }
    }
}
