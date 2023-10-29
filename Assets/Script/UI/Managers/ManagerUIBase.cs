using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Systems.Input_Manager;
using Systems.Scenes_Controller.Concrete_Types;
using UI.Loading_Window;
using UI.Managers.Concrete_Types.In_Game;
using UI.Managers.UI_Windows_Stack_Manager;
using UI.Window;

namespace UI.Managers
{
    public abstract class ManagerUIBase : InitializableMonoBehaviourBase,
        IUIWindowManager,
        IUIManagerInitializationStatus
    {
        protected abstract ILoadingWindow LoadingWindow { get; }
        protected abstract IUIWindowsStackManager WindowsManager { get; }
        protected abstract IScenesController ScenesController { get; }
        protected abstract IInputManagerForUI InputManagerForUI { get; }

        public void OpenWindow(IUIWindow windowToOpen)
        {
            WindowsManager.Open(windowToOpen);
        }

        public void TryCloseCurrentWindow()
        {
            WindowsManager.TryCloseCurrentElement();
        }

        protected override void SubscribeOnEvents()
        {
            ScenesController.LoadingNextSceneStarted += OnLoadingNextSceneStarted;
            InputManagerForUI.CloseCurrentWindow += TryCloseCurrentWindow;
        }

        protected override void UnsubscribeFromEvents()
        {
            ScenesController.LoadingNextSceneStarted -= OnLoadingNextSceneStarted;
            InputManagerForUI.CloseCurrentWindow -= TryCloseCurrentWindow;
        }

        private void OnLoadingNextSceneStarted()
        {
            WindowsManager.Open(LoadingWindow);
        }
    }
}