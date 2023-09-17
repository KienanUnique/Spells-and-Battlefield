using UI.Managers.UI_Windows_Stack_Manager;

namespace UI.Managers.Concrete_Types.Default
{
    public class DefaultManagerUI : ManagerUIBase, IInitializableDefaultManagerUI
    {
        private IUIWindowsStackManager _windowsManager;

        public void Initialize(IUIWindowsStackManager windowsManager)
        {
            _windowsManager = windowsManager;
            SetInitializedStatus();
        }

        protected override IUIWindowsStackManager WindowsManager => _windowsManager;
    }
}