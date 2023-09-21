using System;

namespace Systems.Input_Manager
{
    public interface IInputManagerForUI
    {
        public event Action CloseCurrentWindow;
    }
}