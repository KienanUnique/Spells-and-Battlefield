using System;

namespace UI.Managers.In_Game
{
    public interface IInGameManagerUI
    {
        public event Action RestartLevelRequested;
        public event Action QuitToMainMenuRequested;
        public event Action LoadNextLevelRequested;
        public event Action AllMenusClosed;
        public void SwitchTo(InGameUIElementsGroup needElementsGroup);
    }
}