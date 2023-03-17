using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerLook))]
[RequireComponent(typeof(PlayerSpellsManager))]
[RequireComponent(typeof(IdHolder))]
public class PlayerController : MonoBehaviour, IPlayer
{
    public int Id => _idHolder.Id;
    public Transform MainTransform => _playerMovement.LocalTransform;

    [SerializeField] private PlayerCharacter _playerCharacter = new PlayerCharacter();
    [SerializeField] private PlayerVisual _playerVisual;
    private PlayerSpellsManager _playerSpellsManager;
    private PlayerInputManager _playerInputManager;
    private PlayerMovement _playerMovement;
    private PlayerLook _playerLook;
    private IdHolder _idHolder;

    private void Awake()
    {
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
            _playerSpellsManager.UseSelectedSpell(_playerLook.CameraRotation);
        }
    }

    public void HandleHeal(int countOfHealthPoints)
    {
        _playerCharacter.HandleHeal(countOfHealthPoints);
    }

    public void HandleDamage(int countOfHealthPoints)
    {
        _playerCharacter.HandleDamage(countOfHealthPoints);
    }
}
