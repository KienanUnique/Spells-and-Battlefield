using System;

namespace UI.Managers.Concrete_Types.In_Game
{
    public interface IInGameManagerUI
    {
        public event Action AllMenusClosed;
        public void SwitchTo(InGameUIElementsGroup needElementsGroup);
        public void TryCloseCurrentWindow();
    }
}