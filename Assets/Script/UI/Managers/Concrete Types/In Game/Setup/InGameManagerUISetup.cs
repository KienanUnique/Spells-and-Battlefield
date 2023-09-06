﻿using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Gameplay_UI.Presenter;
using UI.In_Game_Menu.Concrete_Types.Game_Over_Menu.Presenter;
using UI.In_Game_Menu.Concrete_Types.Pause_Menu.Presenter;
using UI.Managers.UI_Windows_Stack_Manager;
using UI.Menu.Concrete_Types.Level_Completed_Menu.Presenter;
using UnityEngine;

namespace UI.Managers.Concrete_Types.In_Game.Setup
{
    public class InGameManagerUISetup : SetupMonoBehaviourBase
    {
        [SerializeField] private GameplayUIPresenter _gameplayUI;
        [SerializeField] private GameOverPresenter _gameOverMenu;
        [SerializeField] private PauseInGameMenuPresenter _pauseMenu;
        [SerializeField] private LevelCompletedMenuPresenter _levelCompletedMenu;
        private IInitializableInGameManagerUI _presenter;

        private IUIWindowsStackManager _stackManager;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable> {_gameplayUI, _gameOverMenu, _pauseMenu, _levelCompletedMenu};

        protected override void Initialize()
        {
            _stackManager = new UIWindowsStackManager(_gameplayUI);
            _presenter.Initialize(_gameplayUI, _gameOverMenu, _pauseMenu, _levelCompletedMenu, _stackManager);
        }

        protected override void Prepare()
        {
            _presenter = GetComponent<IInitializableInGameManagerUI>();
        }
    }
}