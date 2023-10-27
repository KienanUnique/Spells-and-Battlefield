using System;
using UnityEngine.InputSystem;

namespace Systems.Input_Manager.Concrete_Types.Comics_Cutscene
{
    public class ComicsCutsceneInputManager : InputManagerBase, IComicsCutsceneInputManager
    {
        public event Action SkipAnimation;

        protected override void OnEnable()
        {
            base.OnEnable();
            Controls.Comics.SkipPanelAnimation.performed += OnSkipPanelAnimationPerformed;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Controls.Comics.SkipPanelAnimation.performed -= OnSkipPanelAnimationPerformed;
        }

        private void OnSkipPanelAnimationPerformed(InputAction.CallbackContext obj)
        {
            SkipAnimation?.Invoke();
        }
    }
}