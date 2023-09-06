using System;
using UI.Window;

namespace UI.Managers.UI_Windows_Stack_Manager
{
    public interface IUIWindowsStackManager
    {
        public event Action WindowClosed;
        public IUIWindow CurrentOpenedWindow { get; }
        public void Open(IUIWindow window);
        public void TryCloseCurrentElement();
    }
}