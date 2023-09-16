using System.Collections.Generic;
using Common.Abstract_Bases;
using Player;
using Systems.In_Game_Systems.Post_Processing_Controller.Settings;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Systems.In_Game_Systems.Post_Processing_Controller.Setup
{
    [RequireComponent(typeof(Volume))]
    public class PostProcessingControllerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private Volume _dashEffectsVolume;
        private IInitializablePostProcessingController _controller;
        private IPlayerInformationProvider _playerInformationProvider;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private IPostProcessingControllerSettings _settings;

        [Inject]
        private void GetDependencies(IPlayerInformationProvider playerInformationProvider,
            IPostProcessingControllerSettings settings, IPlayerInitializationStatus playerInitializationStatus)
        {
            _playerInformationProvider = playerInformationProvider;
            _settings = settings;
            _playerInitializationStatus = playerInitializationStatus;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new[] {_playerInitializationStatus};

        protected override void Initialize()
        {
            _controller.Initialize(_playerInformationProvider, _dashEffectsVolume, _settings);
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializablePostProcessingController>();
        }
    }
}