using System.Collections.Generic;
using System.Linq;
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
        private IPlayerInformationProvider _playerInformationProvider;
        private ITimeControllerSettings _settings;
        private IInitializableTimeController _controller;
        private IPlayerInitializationStatus _playerInitializationStatus;

        [Inject]
        private void Construct(IPlayerInformationProvider playerInformationProvider, ITimeControllerSettings settings,
            IPlayerInitializationStatus playerInitializationStatus)
        {
            _playerInformationProvider = playerInformationProvider;
            _settings = settings;
            _playerInitializationStatus = playerInitializationStatus;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new[] {_playerInitializationStatus};

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableTimeController>();
        }

        protected override void Initialize()
        {
            _controller.Initialize(_playerInformationProvider, _settings);
        }
    }
}