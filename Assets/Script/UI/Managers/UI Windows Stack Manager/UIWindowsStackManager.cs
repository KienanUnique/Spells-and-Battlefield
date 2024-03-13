using System;
using System.Collections.Generic;
using ModestTree;
using UI.Window;

namespace UI.Managers.UI_Windows_Stack_Manager
{
    public class UIWindowsStackManager : IUIWindowsStackManager
    {
        private readonly Stack<IUIWindow> _windowsStack = new();

        public UIWindowsStackManager(IUIWindow window)
        {
            Open(window);
        }

        public event Action WindowClosed;
        public IUIWindow CurrentOpenedWindow => _windowsStack.Peek();

        public void Open(IUIWindow window)
        {
            if (!_windowsStack.IsEmpty())
            {
                CurrentOpenedWindow.Disappear();
            }

            _windowsStack.Push(window);
            window.Appear();
        }

        public void TryCloseCurrentElement()
        {
            if (!CurrentOpenedWindow.CanBeClosedByPlayer)
            {
                return;
            }

            CurrentOpenedWindow.Disappear();
            _windowsStack.Pop();
            WindowClosed?.Invoke();
            if (!_windowsStack.IsEmpty())
            {
                CurrentOpenedWindow.Appear();
            }
        }
    }
}