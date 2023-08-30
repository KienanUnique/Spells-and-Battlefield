using System;

namespace Systems.Input_Manager
{
    public interface IInGameSystemInputManager
    {
        public event Action GamePauseInputted;
        public event Action CloseCurrentWindow;
        public void SwitchToUIInput();
        public void SwitchToGameInput();
    }
}