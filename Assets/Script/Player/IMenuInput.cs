using System;

namespace Player
{
    public interface IMenuInput
    {
        event Action GamePause;
        event Action GameContinue;
    }
}