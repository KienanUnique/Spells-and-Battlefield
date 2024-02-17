using System;
using Systems.Dialog.Provider;

namespace Systems.Dialog
{
    public class DialogService : IDialogStarterForGameManager, IDialogService
    {
        public event Action<IDialogProvider> NeedStartDialog;
        public event Action DialogStarted;
        public event Action DialogEnded;

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