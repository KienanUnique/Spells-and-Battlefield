using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement = new PlayerMovement();
    [SerializeField] private PlayerLook _playerLook = new PlayerLook();
    private PlayerInputManager _playerInputManager;
    private List<IModelMonoBehaviour> monoBehaviourModels;

    private void Awake()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
        monoBehaviourModels = new List<IModelMonoBehaviour>();
        monoBehaviourModels.Add(_playerMovement);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void FixedUpdate() {
        monoBehaviourModels.ForEach(model => model.FixedUpdate());
    }

    private void OnEnable()
    {
        _playerInputManager.JumpEvent += _playerMovement.Jump;
        _playerInputManager.MoveInputEvent += _playerMovement.Move;
        _playerInputManager.MouseLookEvent += _playerLook.LookWithMouse;
    }

    private void OnDisable()
    {
        _playerInputManager.JumpEvent -= _playerMovement.Jump;
        _playerInputManager.MoveInputEvent -= _playerMovement.Move;
        _playerInputManager.MouseLookEvent -= _playerLook.LookWithMouse;
    }
}
