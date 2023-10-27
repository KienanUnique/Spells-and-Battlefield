using Systems.Scene_Switcher.Concrete_Types;
using UI.Concrete_Scenes.Credits.Credits_Window.Model;
using UI.Concrete_Scenes.Credits.Credits_Window.Presenter;
using UI.Window.Setup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Concrete_Scenes.Credits.Credits_Window.Setup
{
    public class CreditsWindowPresenterSetup : DefaultWindowPresenterSetupBase
    {
        [SerializeField] private Button _quitToMainMenuButton;
        private IInitializableCreditsWindowPresenter _presenter;
        private ICreditsWindowModel _model;
        private IScenesController _scenesController;

        [Inject]
        private void GetDependencies(IScenesController scenesController)
        {
            _scenesController = scenesController;
        }

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableCreditsWindowPresenter>();
            _model = new CreditsWindowModel(IDHolder, _scenesController);
        }

        protected override void Initialize()
        {
            _presenter.Initialize(_model, View, _quitToMainMenuButton);
        }
    }
}