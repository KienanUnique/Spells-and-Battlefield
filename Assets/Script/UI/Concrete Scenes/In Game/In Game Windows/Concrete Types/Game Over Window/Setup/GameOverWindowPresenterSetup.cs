using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Game_Over_Window.Model;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Game_Over_Window.Presenter;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Game_Over_Window.Setup
{
    public class GameOverWindowPresenterSetup : InGameWindowPresenterSetupBase
    {
        private IGameOverWindowModel _model;
        private IInitializableGameOverPresenter _presenter;

        protected override void Initialize()
        {
            _model = new GameOverWindowModel(IDHolder, Manager, InGameSceneController);
            _presenter.Initialize(View, _model, new List<IDisableable>(), RestartLevelButton, GoToMainWindowButton);
        }

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableGameOverPresenter>();
        }
    }
}