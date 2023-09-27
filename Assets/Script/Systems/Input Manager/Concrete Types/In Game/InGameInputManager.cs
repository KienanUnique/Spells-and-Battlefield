using System;
using System.Collections;
using Systems.Input_Manager.Concrete_Types.In_Game.Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Systems.Input_Manager.Concrete_Types.In_Game
{
    public class InGameInputManager : InputManagerBase, IPlayerInput, IInGameSystemInputManager
    {
        private const float MinimalInputMagnitude = 0.5f;

        private float _mouseX, _mouseY;
        private IInputManagerSettings _settings;

        [Inject]
        private void GetDependencies(IInputManagerSettings settings)
        {
            _settings = settings;
        }

        public event Action GamePauseInputted;
        public event Action JumpInputted;
        public event Action StartDashAimingInputted;
        public event Action DashInputted;
        public event Action UseSpellInputted;
        public event Action<Vector2> MoveInputted;
        public event Action<Vector2> LookInputted;
        public event Action<int> SelectSpellTypeWithIndex;
        public event Action SelectNextSpellType;
        public event Action SelectPreviousSpellType;

        public new void SwitchToUIInput()
        {
            base.SwitchToUIInput();
        }

        public void SwitchToGameInput()
        {
            Controls.UI.Disable();
            Controls.Character.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Start()
        {
            StartCoroutine(UpdateLookData());
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Controls.Character.Jump.performed += OnJumpPerformed;
            Controls.Character.Dash.started += OnDashStarted;
            Controls.Character.Dash.canceled += OnDashCanceled;
            Controls.Character.UseSpell.performed += OnUseSpellPerformed;
            Controls.Character.PauseGame.performed += OnPauseGamePerformed;
            Controls.Character.SwitchToSpellTypeWithIndex0.performed += OnPerformedSwitchToSpellTypeWithIndex0;
            Controls.Character.SwitchToSpellTypeWithIndex1.performed += OnPerformedSwitchToSpellTypeWithIndex1;
            Controls.Character.SwitchToSpellTypeWithIndex2.performed += OnPerformedSwitchToSpellTypeWithIndex2;
            Controls.Character.SwitchToSpellTypeWithIndex3.performed += OnPerformedSwitchToSpellTypeWithIndex3;
            Controls.Character.SwitchSpellType.performed += OnSwitchSpellType;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Controls.Character.Jump.performed -= OnJumpPerformed;
            Controls.Character.Dash.started -= OnDashStarted;
            Controls.Character.Dash.canceled -= OnDashCanceled;
            Controls.Character.UseSpell.performed -= OnUseSpellPerformed;
            Controls.Character.PauseGame.performed -= OnPauseGamePerformed;
            Controls.Character.SwitchToSpellTypeWithIndex0.performed -= OnPerformedSwitchToSpellTypeWithIndex0;
            Controls.Character.SwitchToSpellTypeWithIndex1.performed -= OnPerformedSwitchToSpellTypeWithIndex1;
            Controls.Character.SwitchToSpellTypeWithIndex2.performed -= OnPerformedSwitchToSpellTypeWithIndex2;
            Controls.Character.SwitchToSpellTypeWithIndex3.performed -= OnPerformedSwitchToSpellTypeWithIndex3;
            Controls.Character.SwitchSpellType.performed -= OnSwitchSpellType;
        }

        private void OnJumpPerformed(InputAction.CallbackContext obj)
        {
            JumpInputted?.Invoke();
        }

        private void OnDashStarted(InputAction.CallbackContext obj)
        {
            StartDashAimingInputted?.Invoke();
        }

        private void OnDashCanceled(InputAction.CallbackContext obj)
        {
            DashInputted?.Invoke();
        }

        private void OnUseSpellPerformed(InputAction.CallbackContext obj)
        {
            UseSpellInputted?.Invoke();
        }

        private void OnPauseGamePerformed(InputAction.CallbackContext obj)
        {
            GamePauseInputted?.Invoke();
        }

        private void OnSwitchSpellType(InputAction.CallbackContext obj)
        {
            var readValue = Controls.Character.SwitchSpellType.ReadValue<float>();
            if (readValue > 0)
            {
                SelectPreviousSpellType?.Invoke();
            }
            else
            {
                SelectNextSpellType?.Invoke();
            }
        }

        private void OnPerformedSwitchToSpellTypeWithIndex0(InputAction.CallbackContext obj)
        {
            InvokeSwitchToSpellType(0);
        }

        private void OnPerformedSwitchToSpellTypeWithIndex1(InputAction.CallbackContext obj)
        {
            InvokeSwitchToSpellType(1);
        }

        private void OnPerformedSwitchToSpellTypeWithIndex2(InputAction.CallbackContext obj)
        {
            InvokeSwitchToSpellType(2);
        }

        private void OnPerformedSwitchToSpellTypeWithIndex3(InputAction.CallbackContext obj)
        {
            InvokeSwitchToSpellType(3);
        }

        private void InvokeSwitchToSpellType(int typeIndex)
        {
            SelectSpellTypeWithIndex?.Invoke(typeIndex);
        }

        private IEnumerator UpdateLookData()
        {
            Vector2 readDirection;
            while (true)
            {
                readDirection = Controls.Character.Move.ReadValue<Vector2>().normalized;
                MoveInputted?.Invoke(readDirection.magnitude > MinimalInputMagnitude ? readDirection : Vector2.zero);
                LookInputted?.Invoke(_settings.InGameMouseSensitivity *
                                     Time.unscaledDeltaTime *
                                     Controls.Character.Look.ReadValue<Vector2>());
                yield return null;
            }
        }
    }
}