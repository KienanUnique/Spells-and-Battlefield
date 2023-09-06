using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.In_Game_Menu.Concrete_Types.Level_Completed_Menu.Model;
using UI.In_Game_Menu.Concrete_Types.Level_Completed_Menu.Presenter;
using UnityEngine;
using UnityEngine.UI;

namespace UI.In_Game_Menu.Concrete_Types.Level_Completed_Menu.Setup
{
    public class LevelCompletedMenuPresenterSetup : InGameMenuPresenterSetupBase
    {
        [SerializeField] private Button _loadNextLevelButton;
        private ILevelCompletedMenuModel _model;
        private IInitializableLevelCompletedMenuPresenter _presenter;

        protected override void Initialize()
        {
            _model = new LevelCompletedMenuModel(IDHolder, Manager, InGameSceneSwitcher, LoadingWindow);
            _presenter.Initialize(View, _model, new List<IDisableable>(), RestartLevelButton, GoToMainMenuButton,
                _loadNextLevelButton);
        }

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableLevelCompletedMenuPresenter>();
        }
    }
}