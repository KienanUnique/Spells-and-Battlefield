using System;
using Systems.Input_Manager.Concrete_Types.In_Game;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.Input_Manager
{
    public abstract class InputManagerBase : MonoBehaviour, IInputManagerForUI
    {
        public event Action CloseCurrentWindow;
        protected MainControls Controls { get; private set; }

        private void Awake()
        {
            Controls = new MainControls();
        }

        protected virtual void OnEnable()
        {
            Controls.Enable();
            Controls.UI.ContinueGame.performed += OnCloseCurrentWindowPerformed;
        }

        protected virtual void OnDisable()
        {
            Controls.Disable();
            Controls.UI.ContinueGame.performed -= OnCloseCurrentWindowPerformed;
        }
        
        protected void SwitchToUIInput()
        {
            Controls.Character.Disable();
            Controls.UI.Enable();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        private void OnCloseCurrentWindowPerformed(InputAction.CallbackContext obj)
        {
            CloseCurrentWindow?.Invoke();
        }
    }
}