using System;
using Systems.Dialog.Provider;

namespace Systems.Dialog
{
    public interface IDialogStarterForGameManager
    {
        public event Action<IDialogProvider> NeedStartDialog;
        public void HandleDialogEnd();
    }
}