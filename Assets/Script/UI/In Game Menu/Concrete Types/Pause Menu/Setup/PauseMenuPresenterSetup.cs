using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.In_Game_Menu.Concrete_Types.Pause_Menu.Model;
using UI.Menu.Concrete_Types.Pause_Menu.Presenter;
using UnityEngine;
using UnityEngine.UI;

namespace UI.In_Game_Menu.Concrete_Types.Pause_Menu.Setup
{
    public class PauseMenuPresenterSetup : InGameMenuPresenterSetupBase
    {
        [SerializeField] private Button _continueGameButton;
        private IPauseMenuModel _model;
        private IInitializablePauseMenuPresenter _presenter;

        protected override void Initialize()
        {
            _model = new PauseMenuModel(IDHolder, Manager, InGameSceneSwitcher, LoadingWindow);
            _presenter.Initialize(View, _model, new List<IDisableable>(), RestartLevelButton, GoToMainMenuButton,
                _continueGameButton);
        }

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializablePauseMenuPresenter>();
        }
    }
}