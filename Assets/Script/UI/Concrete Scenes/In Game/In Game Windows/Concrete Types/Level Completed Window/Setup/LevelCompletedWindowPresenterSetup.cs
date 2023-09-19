using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window.Model;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window.Presenter;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window.Setup
{
    public class LevelCompletedWindowPresenterSetup : InGameWindowPresenterSetupBase
    {
        [SerializeField] private Button _loadNextLevelButton;
        private ILevelCompletedWindowModel _model;
        private IInitializableLevelCompletedWindowPresenter _presenter;

        protected override void Initialize()
        {
            _model = new LevelCompletedWindowModel(IDHolder, Manager, InGameSceneController);
            _presenter.Initialize(View, _model, new List<IDisableable>(), RestartLevelButton, GoToMainWindowButton,
                _loadNextLevelButton);
        }

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableLevelCompletedWindowPresenter>();
        }
    }
}