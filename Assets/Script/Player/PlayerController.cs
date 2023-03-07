using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour, ICharacter
{
    [SerializeField] private PlayerLook _playerLook = new PlayerLook();
    [SerializeField] private PlayerCharacter _playerCharacter = new PlayerCharacter();
    [SerializeField] private ArmsVisual _playerVisual;
    [SerializeField] private PlayerSpellsManager _playerSpellsManager = new PlayerSpellsManager();
    private PlayerInputManager _playerInputManager;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
        _playerMovement = GetComponent<PlayerMovement>();
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
            _playerSpellsManager.UseSelectedSpell(this, _playerMovement.LocalTransform, _playerLook.CameraRotation);
        }
    }

    public void HandleHeal(int countOfHealPoints) => _playerCharacter.HandleHeal(countOfHealPoints);

    public void HandleDamage(int countOfHealPoints) => _playerCharacter.HandleDamage(countOfHealPoints);

    public void HandleVelocityBoost() => Debug.Log($"Player -> HandleVelocityBoost");
}
