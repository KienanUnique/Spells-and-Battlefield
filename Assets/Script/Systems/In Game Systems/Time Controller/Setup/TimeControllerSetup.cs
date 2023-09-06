using System.Collections.Generic;
using Common.Abstract_Bases;
using Interfaces;
using Player;
using Systems.In_Game_Systems.Time_Controller.Settings;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Systems.In_Game_Systems.Time_Controller.Setup
{
    public class TimeControllerSetup : SetupMonoBehaviourBase
    {
        private IInitializableTimeController _controller;
        private IPlayerInformationProvider _playerInformationProvider;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private ITimeControllerSettings _settings;

        [Inject]
        private void GetDependencies(IPlayerInformationProvider playerInformationProvider,
            ITimeControllerSettings settings, IPlayerInitializationStatus playerInitializationStatus)
        {
            _playerInformationProvider = playerInformationProvider;
            _settings = settings;
            _playerInitializationStatus = playerInitializationStatus;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new[] {_playerInitializationStatus};

        protected override void Initialize()
        {
            _controller.Initialize(_playerInformationProvider, _settings);
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableTimeController>();
        }
    }
}