using System;
using System.Collections;
using Spells.Implementations_Interfaces.Implementations;
using Spells.Spell_Types_Settings;
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
        private ISpellTypesSetting _spellTypesSetting;

        [Inject]
        private void GetDependencies(ISpellTypesSetting spellTypesSetting, IInputManagerSettings settings)
        {
            _spellTypesSetting = spellTypesSetting;
            _settings = settings;
        }

        public event Action GamePauseInputted;
        public event Action JumpInputted;
        public event Action StartDashAimingInputted;
        public event Action DashInputted;
        public event Action UseSpellInputted;
        public event Action<Vector2> MoveInputted;
        public event Action<Vector2> LookInputted;
        public event Action<ISpellType> SelectSpellType;

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
            Controls.Character.SwitchToSpellType1.performed += OnPerformedSwitchToSpellType1;
            Controls.Character.SwitchToSpellType2.performed += OnPerformedSwitchToSpellType2;
            Controls.Character.SwitchToSpellType3.performed += OnPerformedSwitchToSpellType3;
            Controls.Character.SwitchToLastChanceSpellType.performed += OnPerformedSwitchToLastChanceSpellType;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Controls.Character.Jump.performed -= OnJumpPerformed;
            Controls.Character.Dash.started -= OnDashStarted;
            Controls.Character.Dash.canceled -= OnDashCanceled;
            Controls.Character.UseSpell.performed -= OnUseSpellPerformed;
            Controls.Character.PauseGame.performed -= OnPauseGamePerformed;
            Controls.Character.SwitchToSpellType1.performed -= OnPerformedSwitchToSpellType1;
            Controls.Character.SwitchToSpellType2.performed -= OnPerformedSwitchToSpellType2;
            Controls.Character.SwitchToSpellType3.performed -= OnPerformedSwitchToSpellType3;
            Controls.Character.SwitchToLastChanceSpellType.performed -= OnPerformedSwitchToLastChanceSpellType;
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

        private void OnPerformedSwitchToLastChanceSpellType(InputAction.CallbackContext obj)
        {
            SelectSpellType?.Invoke(_spellTypesSetting.LastChanceSpellType);
        }

        private void OnPerformedSwitchToSpellType1(InputAction.CallbackContext obj)
        {
            InvokeSwitchToSpellType(0);
        }

        private void OnPerformedSwitchToSpellType2(InputAction.CallbackContext obj)
        {
            InvokeSwitchToSpellType(1);
        }

        private void OnPerformedSwitchToSpellType3(InputAction.CallbackContext obj)
        {
            InvokeSwitchToSpellType(2);
        }

        private void InvokeSwitchToSpellType(int typeNumber)
        {
            SelectSpellType?.Invoke(_spellTypesSetting.TypesListInOrder[typeNumber]);
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