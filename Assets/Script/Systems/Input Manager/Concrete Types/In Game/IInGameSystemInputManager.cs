using System;

namespace Systems.Input_Manager.Concrete_Types.In_Game
{
    public interface IInGameSystemInputManager
    {
        public event Action GamePauseInputted;
        public void SwitchToUIInput();
        public void SwitchToGameInput();
    }
}