using System.Collections.Generic;
using Common.Abstract_Bases;
using Systems.Input_Manager;
using Systems.Scenes_Controller.Concrete_Types;
using UI.Concrete_Scenes.In_Game.Gameplay_UI.Presenter;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window.Presenter;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Game_Over_Window.Presenter;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window.Presenter;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window.Presenter;
using UI.Loading_Window.Presenter;
using UI.Managers.UI_Windows_Stack_Manager;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Managers.Concrete_Types.In_Game.Setup
{
    public class InGameManagerUISetup : SetupMonoBehaviourBase
    {
        [SerializeField] private GameplayUIPresenter _gameplayUI;
        [SerializeField] private GameOverPresenter _gameOverWindow;
        [SerializeField] private PauseInGameWindowPresenter _pauseWindow;
        [SerializeField] private LevelCompletedWindowPresenter _levelCompletedWindow;
        [SerializeField] private LoadingWindowPresenter _loadingWindow;
        [SerializeField] private DialogWindowPresenter _dialogWindow;

        private IInitializableInGameManagerUI _presenter;
        private IScenesController _scenesController;
        private IInputManagerForUI _inputManagerForUI;
        private IUIWindowsStackManager _stackManager;

        [Inject]
        private void GetDependencies(IScenesController scenesController, IInputManagerForUI inputManagerForUI)
        {
            _scenesController = scenesController;
            _inputManagerForUI = inputManagerForUI;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>
            {
                _gameplayUI,
                _gameOverWindow,
                _pauseWindow,
                _levelCompletedWindow,
                _loadingWindow,
                _dialogWindow
            };

        protected override void Initialize()
        {
            _stackManager = new UIWindowsStackManager(_gameplayUI);
            _presenter.Initialize(_inputManagerForUI, _gameplayUI, _gameOverWindow, _pauseWindow, _levelCompletedWindow,
                _scenesController, _loadingWindow, _dialogWindow, _stackManager);
        }

        protected override void Prepare()
        {
            _presenter = GetComponent<IInitializableInGameManagerUI>();
        }
    }
}