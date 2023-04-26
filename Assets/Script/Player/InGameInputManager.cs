using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InGameInputManager : MonoBehaviour
    {
        private const float MinimalInputMagnitude = 0.5f;

        private MainControls _mainControls;
        private float _mouseX, _mouseY;

        public event Action JumpEvent;
        public event Action DashAimingEvent;
        public event Action DashEvent;
        public event Action UseSpellEvent;
        public event Action<Vector2> MoveInputEvent;
        public event Action<Vector2> MouseLookEvent;

        private void OnJumpPerformed(InputAction.CallbackContext obj) => JumpEvent?.Invoke();
        private void OnDashStarted(InputAction.CallbackContext obj) => DashAimingEvent?.Invoke();
        private void OnDashCanceled(InputAction.CallbackContext obj) => DashEvent?.Invoke();
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

        private void Start()
        {
            StartCoroutine(UpdateLookData());
        }

        private void OnEnable()
        {
            _mainControls.Enable();
            _mainControls.Character.Jump.performed += OnJumpPerformed;
            _mainControls.Character.Dash.started += OnDashStarted;
            _mainControls.Character.Dash.canceled += OnDashCanceled;
            _mainControls.Character.UseSpell.performed += OnUseSpellPerformed;
        }

        private void OnDisable()
        {
            _mainControls.Disable();
            _mainControls.Character.Jump.performed -= OnJumpPerformed;
            _mainControls.Character.Dash.performed -= OnDashStarted;
            _mainControls.Character.UseSpell.performed -= OnUseSpellPerformed;
        }

        private IEnumerator UpdateLookData()
        {
            Vector2 readDirection;
            while (true)
            {
                readDirection = _mainControls.Character.Move.ReadValue<Vector2>().normalized;
                MoveInputEvent?.Invoke(readDirection.magnitude > MinimalInputMagnitude ? readDirection : Vector2.zero);
                MouseLookEvent?.Invoke(_mainControls.Character.Look.ReadValue<Vector2>());
                yield return null;
            }
        }
    }
}