using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Main_Menu_Window.Model;
using UI.Window.Model;
using UI.Window.Presenter;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Main_Menu_Window.Presenter
{
    public class MainMenuWindowPresenter : WindowPresenterBase, IInitializableMainMenuWindowPresenter
    {
        private IMainMenuWindowModel _model;
        private IUIWindowView _view;

        public void Initialize(IMainMenuWindowModel model, IUIWindowView view, Button startGameButton,
            Button creditsButton, Button quitButton)
        {
            _model = model;
            _view = view;
            startGameButton.onClick.AddListener(_model.OnStartGameButtonPressed);
            creditsButton.onClick.AddListener(_model.OnCreditsButtonPressed);
            quitButton.onClick.AddListener(_model.OnQuitButtonPressed);
            SetInitializedStatus();
        }

        protected override IUIWindowView WindowView => _view;
        protected override IUIWindowModel WindowModel => _model;
    }
}