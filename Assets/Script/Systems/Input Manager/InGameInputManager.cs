using System;
using System.Collections;
using Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Input_Manager
{
    public class InGameInputManager : Singleton<InGameInputManager>, IPlayerInput, IMenuInput
    {
        private const float MinimalInputMagnitude = 0.5f;

        [SerializeField] private float _inGameMouseSensitivity = 21f;

        private MainControls _mainControls;
        private float _mouseX, _mouseY;

        public event Action JumpInputted;
        public event Action StartDashAimingInputted;
        public event Action DashInputted;
        public event Action UseSpellInputted;
        public event Action<Vector2> MoveInputted;
        public event Action<Vector2> LookInputted;
        public event Action GamePause;
        public event Action GameContinue;

        private void OnJumpPerformed(InputAction.CallbackContext obj) => JumpInputted?.Invoke();
        private void OnDashStarted(InputAction.CallbackContext obj) => StartDashAimingInputted?.Invoke();
        private void OnDashCanceled(InputAction.CallbackContext obj) => DashInputted?.Invoke();
        private void OnUseSpellPerformed(InputAction.CallbackContext obj) => UseSpellInputted?.Invoke();
        private void OnPauseGamePerformed(InputAction.CallbackContext obj) => GamePause?.Invoke();
        private void OnContinueGamePerformed(InputAction.CallbackContext obj) => GameContinue?.Invoke();

        public void SwitchToUIInput()
        {
            _mainControls.Character.Disable();
            _mainControls.UI.Enable();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void SwitchToGameInput()
        {
            _mainControls.UI.Disable();
            _mainControls.Character.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        protected override void SpecialAwakeAction()
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
            _mainControls.Character.PauseGame.performed += OnPauseGamePerformed;
            _mainControls.UI.ContinueGame.performed += OnContinueGamePerformed;
        }

        private void OnDisable()
        {
            _mainControls.Disable();
            _mainControls.Character.Jump.performed -= OnJumpPerformed;
            _mainControls.Character.Dash.started -= OnDashStarted;
            _mainControls.Character.Dash.canceled -= OnDashCanceled;
            _mainControls.Character.UseSpell.performed -= OnUseSpellPerformed;
            _mainControls.Character.PauseGame.performed -= OnPauseGamePerformed;
            _mainControls.UI.ContinueGame.performed -= OnContinueGamePerformed;
        }

        private IEnumerator UpdateLookData()
        {
            Vector2 readDirection;
            while (true)
            {
                readDirection = _mainControls.Character.Move.ReadValue<Vector2>().normalized;
                MoveInputted?.Invoke(readDirection.magnitude > MinimalInputMagnitude ? readDirection : Vector2.zero);
                LookInputted?.Invoke(_inGameMouseSensitivity * Time.unscaledDeltaTime *
                                     _mainControls.Character.Look.ReadValue<Vector2>());
                yield return null;
            }
        }
    }
}