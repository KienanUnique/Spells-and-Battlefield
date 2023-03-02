using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Action JumpEvent;
    public Action UseSpellEvent;
    public Action<Vector2> MoveInputEvent;
    public Action StopMoveEvent;
    public Action<Vector2> MouseLookEvent;
    private const float MinimalInputMagnitude = 0.5f;
    private MainControls _mainControls;
    private float _mouseX, _mouseY;

    private void Awake()
    {
        _mainControls = new MainControls();
    }

    private void OnEnable()
    {
        _mainControls.Enable();
        _mainControls.PlayerActions.Jump.performed += OnJumpPerformed;
        _mainControls.PlayerActions.UseSpell.performed += OnUseSpellPerformed;
    }

    private void OnDisable()
    {
        _mainControls.Disable();
        _mainControls.PlayerActions.Jump.performed -= OnJumpPerformed;
        _mainControls.PlayerActions.UseSpell.performed -= OnUseSpellPerformed;
    }

    private void Update()
    {
        var readDirection = _mainControls.PlayerActions.Move.ReadValue<Vector2>().normalized;
        if (readDirection.magnitude > MinimalInputMagnitude)
        {
            MoveInputEvent?.Invoke(readDirection);
        }
        else{
            MoveInputEvent?.Invoke(Vector2.zero);
        }

        MouseLookEvent?.Invoke(_mainControls.PlayerActions.Look.ReadValue<Vector2>());
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj) => JumpEvent?.Invoke();
    private void OnUseSpellPerformed(InputAction.CallbackContext obj) => UseSpellEvent?.Invoke();
}
