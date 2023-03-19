using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        public Action JumpEvent;
        public Action WalkStartEvent;
        public Action WalkCancelEvent;
        public Action UseSpellEvent;
        public Action<Vector2> MoveInputEvent;
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
            _mainControls.PlayerActions.Walk.started += OnWalkStarted;
            _mainControls.PlayerActions.Walk.canceled += OnWalkCanceled;
        }

        private void OnDisable()
        {
            _mainControls.Disable();
            _mainControls.PlayerActions.Jump.performed -= OnJumpPerformed;
            _mainControls.PlayerActions.UseSpell.performed -= OnUseSpellPerformed;
            _mainControls.PlayerActions.Walk.started -= OnWalkStarted;
            _mainControls.PlayerActions.Walk.canceled -= OnWalkCanceled;
        }

        private void Update()
        {
            var readDirection = _mainControls.PlayerActions.Move.ReadValue<Vector2>().normalized;
            MoveInputEvent?.Invoke(readDirection.magnitude > MinimalInputMagnitude ? readDirection : Vector2.zero);

            MouseLookEvent?.Invoke(_mainControls.PlayerActions.Look.ReadValue<Vector2>());
        }

        private void OnWalkStarted(InputAction.CallbackContext obj) => WalkStartEvent?.Invoke();
        private void OnWalkCanceled(InputAction.CallbackContext obj) => WalkCancelEvent?.Invoke();
        private void OnJumpPerformed(InputAction.CallbackContext obj) => JumpEvent?.Invoke();
        private void OnUseSpellPerformed(InputAction.CallbackContext obj) => UseSpellEvent?.Invoke();
    }
}