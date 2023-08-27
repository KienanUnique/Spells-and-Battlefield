using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Menu.Concrete_Types.Game_Over_Menu.Model;
using UI.Menu.Concrete_Types.Game_Over_Menu.Presenter;
using UI.Menu.Setup;

namespace UI.Menu.Concrete_Types.Game_Over_Menu.Setup
{
    public class GameOverMenuPresenterSetup : MenuPresenterSetupBase
    {
        private IGameOverMenuModel _model;
        private IInitializableGameOverPresenter _presenter;

        protected override void Prepare()
        {
            base.Prepare();
            _model = new GameOverMenuModel(_idHolder, _uiControllable);
            _presenter = GetComponent<IInitializableGameOverPresenter>();
        }

        protected override void Initialize()
        {
            _presenter.Initialize(View, _model, new List<IDisableable>(), _restartLevelButton, _goToMainMenuButton);
        }
    }
}