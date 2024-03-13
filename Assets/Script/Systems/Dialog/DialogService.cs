using System;
using Systems.Dialog.Provider;

namespace Systems.Dialog
{
    public class DialogService : IDialogStarterForGameManager, IDialogService
    {
        public event Action DialogStarted;
        public event Action DialogEnded;
        public event Action<IDialogProvider> NeedStartDialog;

        public void StartDialog(IDialogProvider dialog)
        {
            NeedStartDialog?.Invoke(dialog);
            DialogStarted?.Invoke();
        }

        public void HandleDialogEnd()
        {
            DialogEnded?.Invoke();
        }
    }
}