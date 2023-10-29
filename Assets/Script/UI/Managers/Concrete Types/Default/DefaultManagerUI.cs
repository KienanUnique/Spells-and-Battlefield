using Systems.Input_Manager;
using Systems.Scenes_Controller.Concrete_Types;
using UI.Loading_Window;
using UI.Managers.UI_Windows_Stack_Manager;

namespace UI.Managers.Concrete_Types.Default
{
    public class DefaultManagerUI : ManagerUIBase, IInitializableDefaultManagerUI
    {
        private IUIWindowsStackManager _windowsManager;
        private IScenesController _scenesController;
        private ILoadingWindow _loadingWindow;
        private IInputManagerForUI _inputManagerForUI;

        public void Initialize(IInputManagerForUI inputManagerForUI, IUIWindowsStackManager windowsManager,
            IScenesController scenesController, ILoadingWindow loadingWindow)
        {
            _inputManagerForUI = inputManagerForUI;
            _windowsManager = windowsManager;
            _scenesController = scenesController;
            _loadingWindow = loadingWindow;
            SetInitializedStatus();
        }

        protected override ILoadingWindow LoadingWindow => _loadingWindow;
        protected override IUIWindowsStackManager WindowsManager => _windowsManager;
        protected override IScenesController ScenesController => _scenesController;
        protected override IInputManagerForUI InputManagerForUI => _inputManagerForUI;
    }
}