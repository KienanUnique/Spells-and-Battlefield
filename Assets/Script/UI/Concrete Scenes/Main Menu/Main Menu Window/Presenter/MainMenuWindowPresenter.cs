using UI.Concrete_Scenes.Main_Menu.Main_Menu_Window.Model;
using UI.Element.View;
using UI.Window.Model;
using UI.Window.Presenter;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Main_Menu.Main_Menu_Window.Presenter
{
    public class MainMenuWindowPresenter : WindowPresenterBase, IInitializableMainMenuWindowPresenter
    {
        private IMainMenuWindowModel _model;
        private IUIElementView _view;

        public void Initialize(IMainMenuWindowModel model, IUIElementView view, Button startGameButton,
            Button creditsButton, Button quitButton)
        {
            _model = model;
            _view = view;
            startGameButton.onClick.AddListener(_model.OnStartGameButtonPressed);
            creditsButton.onClick.AddListener(_model.OnCreditsButtonPressed);
            quitButton.onClick.AddListener(_model.OnQuitButtonPressed);
            SetInitializedStatus();
        }

        protected override IUIElementView View => _view;
        protected override IUIWindowModel Model => _model;
    }
}