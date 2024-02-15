using System;
using Systems.Dialog.Provider;

namespace UI.Managers.Concrete_Types.In_Game
{
    public interface IInGameManagerUI
    {
        public event Action AllWindowsClosed;
        public void SwitchTo(InGameUIElementsGroup needElementsGroup);
        public void OpenDialog(IDialogProvider dialog);
        public void TryCloseCurrentWindow();
    }
}