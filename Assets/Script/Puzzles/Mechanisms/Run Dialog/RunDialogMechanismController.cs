using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Puzzles.Mechanisms_Triggers;
using Systems.Dialog;
using Systems.Dialog.Provider;
using UnityEngine;

namespace Puzzles.Mechanisms.Run_Dialog
{
    public class RunDialogMechanismController : MechanismControllerBase, IInitializableRunDialogMechanismController
    {
        private List<IDialogProvider> _allDialogs;
        private List<IDialogProvider> _availableDialogs;
        private IDialogService _dialogService;

        public void Initialize(List<IDialogProvider> allDialogs, IDialogService dialogService,
            List<IMechanismsTrigger> triggers)
        {
            _allDialogs = allDialogs;
            _dialogService = dialogService;
            _availableDialogs = new List<IDialogProvider>(allDialogs);
            AddTriggers(triggers);
            SetInitializedStatus();
        }

        protected override void StartJob()
        {
            var randomDialog = SelectRandomDialog();
            _dialogService.StartDialog(randomDialog);
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

        private IDialogProvider SelectRandomDialog()
        {
            if (_availableDialogs.IsEmpty())
            {
                _availableDialogs.AddRange(_allDialogs);
            }

            var dialogIndex = Random.Range(0, _availableDialogs.Count);
            var needDialog = _availableDialogs.ElementAt(dialogIndex);

            _availableDialogs.RemoveAt(dialogIndex);

            return needDialog;
        }
    }
}