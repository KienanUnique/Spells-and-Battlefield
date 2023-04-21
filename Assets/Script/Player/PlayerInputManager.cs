using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        private const float MinimalInputMagnitude = 0.5f;

        private MainControls _mainControls;
        private float _mouseX, _mouseY;

        public event Action JumpEvent;
        public event Action UseSpellEvent;
        public event Action<Vector2> MoveInputEvent;
        public event Action<Vector2> MouseLookEvent;
        
        private void OnJumpPerformed(InputAction.CallbackContext obj) => JumpEvent?.Invoke();
        private void OnUseSpellPerformed(InputAction.CallbackContext obj) => UseSpellEvent?.Invoke();
        
        public void SwitchToUIInput()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _mainControls.Character.Disable();
            _mainControls.UI.Enable();
        }

        public void SwitchToGameInput()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _mainControls.Character.Enable();
            _mainControls.UI.Disable();
        }

        private void Awake()
        {
            _mainControls = new MainControls();
        }

        private void OnEnable()
        {
            _mainControls.Enable();
            _mainControls.Character.Jump.performed += OnJumpPerformed;
            _mainControls.Character.UseSpell.performed += OnUseSpellPerformed;
        }

        private void OnDisable()
        {
            _mainControls.Disable();
            _mainControls.Character.Jump.performed -= OnJumpPerformed;
            _mainControls.Character.UseSpell.performed -= OnUseSpellPerformed;
        }

        private void Update()
        {
            var readDirection = _mainControls.Character.Move.ReadValue<Vector2>().normalized;
            MoveInputEvent?.Invoke(readDirection.magnitude > MinimalInputMagnitude ? readDirection : Vector2.zero);

            MouseLookEvent?.Invoke(_mainControls.Character.Look.ReadValue<Vector2>());
        }
    }
}