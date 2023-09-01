using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.In_Game_Menu.Concrete_Types.Game_Over_Menu.Model;
using UI.In_Game_Menu.Concrete_Types.Game_Over_Menu.Presenter;

namespace UI.In_Game_Menu.Concrete_Types.Game_Over_Menu.Setup
{
    public class GameOverMenuPresenterSetup : InGameMenuPresenterSetupBase
    {
        private IGameOverMenuModel _model;
        private IInitializableGameOverPresenter _presenter;

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableGameOverPresenter>();
        }

        protected override void Initialize()
        {
            _model = new GameOverMenuModel(IDHolder, Manager, InGameSceneSwitcher, LoadingWindow);
            _presenter.Initialize(View, _model, new List<IDisableable>(), RestartLevelButton, GoToMainMenuButton);
        }
    }
}