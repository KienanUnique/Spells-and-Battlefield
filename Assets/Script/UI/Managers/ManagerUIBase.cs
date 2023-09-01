using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Managers.Concrete_Types.In_Game;
using UI.Managers.UI_Windows_Stack_Manager;
using UI.Window;

namespace UI.Managers
{
    public abstract class ManagerUIBase : InitializableMonoBehaviourBase, IUIWindowManager,
        IUIManagerInitializationStatus
    {
        protected abstract IUIWindowsStackManager WindowsManager { get; }

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
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}