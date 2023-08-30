using System;
using System.Collections;
using Settings;
using Spells.Implementations_Interfaces.Implementations;
using Systems.Input_Manager.Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Systems.Input_Manager
{
    public class InGameInputManager : MonoBehaviour, IPlayerInput, IInGameSystemInputManager
    {
        private const float MinimalInputMagnitude = 0.5f;

        private MainControls _mainControls;
        private float _mouseX, _mouseY;
        private SpellTypesSetting _spellTypesSetting;
        private IInputManagerSettings _settings;

        [Inject]
        private void Construct(SpellTypesSetting spellTypesSetting, IInputManagerSettings settings)
        {
            _spellTypesSetting = spellTypesSetting;
            _settings = settings;
        }

        public event Action JumpInputted;
        public event Action StartDashAimingInputted;
        public event Action DashInputted;
        public event Action UseSpellInputted;
        public event Action<Vector2> MoveInputted;
        public event Action<Vector2> LookInputted;
        public event Action<ISpellType> SelectSpellType;
        public event Action GamePauseInputted;
        public event Action CloseCurrentWindow;

        private void OnJumpPerformed(InputAction.CallbackContext obj) => JumpInputted?.Invoke();
        private void OnDashStarted(InputAction.CallbackContext obj) => StartDashAimingInputted?.Invoke();
        private void OnDashCanceled(InputAction.CallbackContext obj) => DashInputted?.Invoke();
        private void OnUseSpellPerformed(InputAction.CallbackContext obj) => UseSpellInputted?.Invoke();
        private void OnPauseGamePerformed(InputAction.CallbackContext obj) => GamePauseInputted?.Invoke();
        private void OnContinueGamePerformed(InputAction.CallbackContext obj) => CloseCurrentWindow?.Invoke();

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
            _mainControls.Character.PauseGame.performed += OnPauseGamePerformed;
            _mainControls.Character.SwitchToSpellType1.performed += OnPerformedSwitchToSpellType1;
            _mainControls.Character.SwitchToSpellType2.performed += OnPerformedSwitchToSpellType2;
            _mainControls.Character.SwitchToSpellType3.performed += OnPerformedSwitchToSpellType3;
            _mainControls.Character.SwitchToLastChanceSpellType.performed += OnPerformedSwitchToLastChanceSpellType;
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
            _mainControls.Character.SwitchToSpellType1.performed -= OnPerformedSwitchToSpellType1;
            _mainControls.Character.SwitchToSpellType2.performed -= OnPerformedSwitchToSpellType2;
            _mainControls.Character.SwitchToSpellType3.performed -= OnPerformedSwitchToSpellType3;
            _mainControls.Character.SwitchToLastChanceSpellType.performed -= OnPerformedSwitchToLastChanceSpellType;
            _mainControls.UI.ContinueGame.performed -= OnContinueGamePerformed;
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
                readDirection = _mainControls.Character.Move.ReadValue<Vector2>().normalized;
                MoveInputted?.Invoke(readDirection.magnitude > MinimalInputMagnitude ? readDirection : Vector2.zero);
                LookInputted?.Invoke(_settings.InGameMouseSensitivity * Time.unscaledDeltaTime *
                                     _mainControls.Character.Look.ReadValue<Vector2>());
                yield return null;
            }
        }
    }
}