using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Gameplay_UI.Presenter;
using UI.Loading_Window.Presenter;
using UI.Managers.UI_Windows_Stack_Manager;
using UI.Menu.Concrete_Types.Game_Over_Menu.Presenter;
using UI.Menu.Concrete_Types.Level_Completed_Menu.Presenter;
using UI.Menu.Concrete_Types.Pause_Menu.Presenter;
using UnityEngine;

namespace UI.Managers.In_Game.Setup
{
    public class InGameManagerUISetup : SetupMonoBehaviourBase
    {
        [SerializeField] private LoadingWindowPresenter _loadingWindow;
        [SerializeField] private GameplayUIPresenter _gameplayUI;
        [SerializeField] private GameOverPresenter _gameOverMenu;
        [SerializeField] private PauseInGameMenuPresenter _pauseMenu;
        [SerializeField] private LevelCompletedMenuPresenter _levelCompletedMenu;

        private IUIWindowsStackManager _stackManager;
        private IInitializableInGameManagerUI _presenter;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization => new List<IInitializable>
        {
            _loadingWindow,
            _gameplayUI,
            _gameOverMenu,
            _pauseMenu,
            _levelCompletedMenu
        };

        protected override void Prepare()
        {
            _presenter = GetComponent<IInitializableInGameManagerUI>();
        }

        protected override void Initialize()
        {
            _stackManager = new UIWindowsStackManager(_gameplayUI);
            _presenter.Initialize(_loadingWindow, _gameplayUI, _gameOverMenu, _pauseMenu, _levelCompletedMenu,
                _stackManager);
        }
    }
}