using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerSpellsManager))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerLook _playerLook = new PlayerLook();
    [SerializeField] private ArmsVisual _playerVisual;
    private PlayerInputManager _playerInputManager;
    private PlayerMovement _playerMovement;
    private PlayerSpellsManager _playerSpellsManager;

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerSpellsManager = GetComponent<PlayerSpellsManager>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        _playerInputManager.JumpEvent += _playerMovement.Jump;
        _playerInputManager.UseSpellEvent += StartUseSelectedSpell;
        _playerInputManager.MoveInputEvent += _playerMovement.Move;
        _playerInputManager.MouseLookEvent += _playerLook.LookWithMouse;
        _playerVisual.UseSpellAnimationMomentStart += UseSelectedSpell;
    }

    private void OnDisable()
    {
        _playerInputManager.JumpEvent -= _playerMovement.Jump;
        _playerInputManager.UseSpellEvent -= StartUseSelectedSpell;
        _playerInputManager.MoveInputEvent -= _playerMovement.Move;
        _playerInputManager.MouseLookEvent -= _playerLook.LookWithMouse;
        _playerVisual.UseSpellAnimationMomentStart -= UseSelectedSpell;
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
            _playerSpellsManager.UseSelectedSpell(_playerMovement.CurrentRotation);
        }
    }
}
