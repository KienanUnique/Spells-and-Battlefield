using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Concrete_Scenes.In_Game.Gameplay_UI.Presenter;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Game_Over_Window.Presenter;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window.Presenter;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window.Presenter;
using UI.Managers.UI_Windows_Stack_Manager;
using UnityEngine;

namespace UI.Managers.Concrete_Types.In_Game.Setup
{
    public class InGameManagerUISetup : SetupMonoBehaviourBase
    {
        [SerializeField] private GameplayUIPresenter _gameplayUI;
        [SerializeField] private GameOverPresenter _gameOverWindow;
        [SerializeField] private PauseInGameWindowPresenter _pauseWindow;
        [SerializeField] private LevelCompletedWindowPresenter _levelCompletedWindow;
        private IInitializableInGameManagerUI _presenter;

        private IUIWindowsStackManager _stackManager;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable> {_gameplayUI, _gameOverWindow, _pauseWindow, _levelCompletedWindow};

        protected override void Initialize()
        {
            _stackManager = new UIWindowsStackManager(_gameplayUI);
            _presenter.Initialize(_gameplayUI, _gameOverWindow, _pauseWindow, _levelCompletedWindow, _stackManager);
        }

        protected override void Prepare()
        {
            _presenter = GetComponent<IInitializableInGameManagerUI>();
        }
    }
}