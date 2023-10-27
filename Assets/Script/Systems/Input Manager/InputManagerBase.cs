using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Input_Manager
{
    public abstract class InputManagerBase : MonoBehaviour, IInputManagerForUI
    {
        public event Action CloseCurrentWindow;
        protected MainControls Controls { get; private set; }

        protected virtual void OnEnable()
        {
            Controls.Enable();
            Controls.UI.CloseWindow.performed += OnCloseCurrentWindowPerformed;
        }

        protected virtual void OnDisable()
        {
            Controls.Disable();
            Controls.UI.CloseWindow.performed -= OnCloseCurrentWindowPerformed;
        }

        protected void SwitchToUIInput()
        {
            Controls.Character.Disable();
            Controls.UI.Enable();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        private void Awake()
        {
            Controls = new MainControls();
        }

        private void OnCloseCurrentWindowPerformed(InputAction.CallbackContext obj)
        {
            CloseCurrentWindow?.Invoke();
        }
    }
}