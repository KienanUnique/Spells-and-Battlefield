using System.Collections.Generic;
using Common.Abstract_Bases;
using Player;
using Systems.Dialog;
using Systems.In_Game_Systems.Post_Processing_Controller.Settings;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Systems.In_Game_Systems.Post_Processing_Controller.Setup
{
    public class PostProcessingControllerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private Volume _dashEffectsVolume;
        [SerializeField] private Volume _dashAimingEffectsVolume;
        [SerializeField] private Volume _dialogEffectsVolume;
        private IInitializablePostProcessingController _controller;
        private IPlayerInformationProvider _playerInformationProvider;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private IPostProcessingControllerSettings _settings;
        private IDialogService _dialogService;

        [Inject]
        private void GetDependencies(IPlayerInformationProvider playerInformationProvider,
            IPostProcessingControllerSettings settings, IPlayerInitializationStatus playerInitializationStatus,
            IDialogService dialogService)
        {
            _playerInformationProvider = playerInformationProvider;
            _settings = settings;
            _playerInitializationStatus = playerInitializationStatus;
            _dialogService = dialogService;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new[] {_playerInitializationStatus};

        protected override void Initialize()
        {
            _controller.Initialize(_playerInformationProvider, _dashEffectsVolume, _dashAimingEffectsVolume,
                _dialogEffectsVolume, _settings, _dialogService);
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializablePostProcessingController>();
        }
    }
}