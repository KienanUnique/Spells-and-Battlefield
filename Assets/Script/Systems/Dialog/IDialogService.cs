using System;
using Systems.Dialog.Provider;

namespace Systems.Dialog
{
    public interface IDialogService
    {
        public event Action DialogStarted;
        public event Action DialogEnded;
        public void StartDialog(IDialogProvider dialog);
    }
}