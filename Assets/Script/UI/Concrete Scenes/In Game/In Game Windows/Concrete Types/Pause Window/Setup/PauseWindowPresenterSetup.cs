using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window.Model;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window.Presenter;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window.Setup
{
    public class PauseWindowPresenterSetup : InGameWindowPresenterSetupBase
    {
        [SerializeField] private Button _continueGameButton;
        private IPauseWindowModel _model;
        private IInitializablePauseWindowPresenter _presenter;

        protected override void Initialize()
        {
            _model = new PauseWindowModel(IDHolder, Manager, InGameSceneManager, LoadingWindow);
            _presenter.Initialize(View, _model, new List<IDisableable>(), RestartLevelButton, GoToMainWindowButton,
                _continueGameButton);
        }

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializablePauseWindowPresenter>();
        }
    }
}