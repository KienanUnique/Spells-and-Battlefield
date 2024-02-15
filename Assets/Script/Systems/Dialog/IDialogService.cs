using System;
using Systems.Dialog.Provider;

namespace Systems.Dialog
{
    public interface IDialogService
    {
        public void StartDialog(IDialogProvider dialog);
        public event Action DialogEnded;
    }
}