using UI.Window;

namespace UI.Managers.Concrete_Types.In_Game
{
    public interface IUIWindowManager
    {
        public void OpenWindow(IUIWindow windowToOpen);
        public void TryCloseCurrentWindow();
    }
}