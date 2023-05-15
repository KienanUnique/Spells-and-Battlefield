using System;

namespace Systems.Input_Manager
{
    public interface IMenuInput
    {
        public event Action GamePause;
        public event Action GameContinue;
    }
}