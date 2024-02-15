using System.Collections.Generic;
using Puzzles.Mechanisms_Triggers;
using Systems.Dialog;
using Systems.Dialog.Provider;

namespace Puzzles.Mechanisms.Run_Dialog
{
    public class RunDialogMechanismController : MechanismControllerBase, IInitializableRunDialogMechanismController
    {
        private IDialogProvider _dialog;
        private IDialogService _dialogService;

        public void Initialize(IDialogProvider dialog, IDialogService dialogService, List<IMechanismsTrigger> triggers)
        {
            _dialog = dialog;
            _dialogService = dialogService;
            AddTriggers(triggers);
            SetInitializedStatus();
        }

        protected override void StartJob()
        {
            _dialogService.StartDialog(_dialog);
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _dialogService.DialogEnded += HandleDoneJob;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            _dialogService.DialogEnded -= HandleDoneJob;
        }
    }
}