using System.Collections.Generic;
using Common.Abstract_Bases;
using Player;
using Systems.Dialog;
using Systems.In_Game_Systems.Level_Finish_Zone;
using Systems.In_Game_Systems.Time_Controller;
using Systems.Input_Manager.Concrete_Types.In_Game;
using UI.Managers.Concrete_Types.In_Game;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Systems.In_Game_Systems.Game_Controller
{
    [RequireComponent(typeof(TimeController))]
    public class GameControllerSetup : SetupMonoBehaviourBase
    {
        private IInitializableGameController _controller;
        private IInGameManagerUI _inGameManagerUI;
        private IUIManagerInitializationStatus _inGameManagerUIInitializationStatus;
        private IInGameSystemInputManager _inGameSystemInput;
        private ILevelFinishZone _levelFinishZone;
        private IPlayerInformationProvider _playerInformationProvider;
        private IPlayerInitializationStatus _playerInitializationStatus;
        private IDialogStarterForGameManager _dialogStarterForGameManager;
        private TimeController _timeController;

        [Inject]
        private void GetDependencies(IPlayerInformationProvider playerInformationProvider,
            IPlayerInitializationStatus playerInitializationStatus, ILevelFinishZone levelFinishZone,
            IInGameSystemInputManager inGameSystemInput,
            IUIManagerInitializationStatus inGameManagerUIInitializationStatus, IInGameManagerUI inGameManagerUI,
            IDialogStarterForGameManager dialogStarterForGameManager)
        {
            _playerInformationProvider = playerInformationProvider;
            _levelFinishZone = levelFinishZone;
            _playerInitializationStatus = playerInitializationStatus;
            _inGameSystemInput = inGameSystemInput;
            _inGameManagerUIInitializationStatus = inGameManagerUIInitializationStatus;
            _inGameManagerUI = inGameManagerUI;
            _dialogStarterForGameManager = dialogStarterForGameManager;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>
            {
                _playerInitializationStatus, _timeController, _inGameManagerUIInitializationStatus
            };

        protected override void Initialize()
        {
            _controller.Initialize(_inGameManagerUI, _playerInformationProvider, _inGameSystemInput, _timeController,
                _levelFinishZone, _dialogStarterForGameManager);
        }

        protected override void Prepare()
        {
            _timeController = GetComponent<TimeController>();
            _controller = GetComponent<IInitializableGameController>();
        }
    }
}