using System.Collections.Generic;
using Puzzles.Mechanisms_Triggers;
using Systems.Dialog;
using Systems.Dialog.Provider;

namespace Puzzles.Mechanisms.Run_Dialog
{
    public interface IInitializableRunDialogMechanismController
    {
        void Initialize(List<IDialogProvider> allDialogs, IDialogService dialogService,
            List<IMechanismsTrigger> triggers);
    }
}