using System.Collections.Generic;
using Common.Abstract_Bases;
using Interfaces;
using Player;
using Systems.In_Game_Systems.Level_Finish_Zone;
using Systems.In_Game_Systems.Time_Controller;
using Systems.Input_Manager;
using Systems.Scene_Switcher.Concrete_Types;
using UI.Managers.In_Game;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Systems.In_Game_Systems.Game_Controller
{
    [RequireComponent(typeof(TimeController))]
    [RequireComponent(typeof(InGameScenesSwitcher))]
    public class GameControllerSetup : SetupMonoBehaviourBase
    {
        private InGameScenesSwitcher _inGameScenesSwitcher;
        private IInGameManagerUI _inGameManagerUI;
        private IInGameSystemInputManager _inGameSystemInput;
        private IPlayerInformationProvider _playerInformationProvider;
        private TimeController _timeController;
        private ILevelFinishZone _levelFinishZone;
        private IInitializableGameController _controller;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private IInGameManagerUIInitializationStatus _inGameManagerUIInitializationStatus;

        [Inject]
        private void Construct(IPlayerInformationProvider playerInformationProvider,
            IPlayerInitializationStatus playerInitializationStatus, ILevelFinishZone levelFinishZone,
            IInGameSystemInputManager inGameSystemInput,
            IInGameManagerUIInitializationStatus inGameManagerUIInitializationStatus, IInGameManagerUI inGameManagerUI)
        {
            _playerInformationProvider = playerInformationProvider;
            _levelFinishZone = levelFinishZone;
            _playerInitializationStatus = playerInitializationStatus;
            _inGameSystemInput = inGameSystemInput;
            _inGameManagerUIInitializationStatus = inGameManagerUIInitializationStatus;
            _inGameManagerUI = inGameManagerUI;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization => new List<IInitializable>
            {_playerInitializationStatus, _timeController, _inGameManagerUIInitializationStatus};

        protected override void Prepare()
        {
            _timeController = GetComponent<TimeController>();
            _inGameScenesSwitcher = GetComponent<InGameScenesSwitcher>();
            _controller = GetComponent<IInitializableGameController>();
        }

        protected override void Initialize()
        {
            _controller.Initialize(_inGameManagerUI, _playerInformationProvider,
                _inGameSystemInput, _timeController, _levelFinishZone, _inGameScenesSwitcher);
        }
    }
}