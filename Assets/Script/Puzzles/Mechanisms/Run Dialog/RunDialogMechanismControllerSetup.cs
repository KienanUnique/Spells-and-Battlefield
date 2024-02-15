using System.Collections.Generic;
using Common.Abstract_Bases;
using Puzzles.Mechanisms_Triggers;
using Systems.Dialog;
using Systems.Dialog.Provider;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Puzzles.Mechanisms.Run_Dialog
{
    public class RunDialogMechanismControllerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private List<MechanismsTriggerBase> _triggers;
        [SerializeField] private DialogProvider _dialog;

        private IDialogService _dialogService;
        private IInitializableRunDialogMechanismController _controller;

        [Inject]
        private void GetDependencies(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization => _triggers;

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableRunDialogMechanismController>();
        }

        protected override void Initialize()
        {
            _controller.Initialize(_dialog, _dialogService, new List<IMechanismsTrigger>(_triggers));
        }
    }
}