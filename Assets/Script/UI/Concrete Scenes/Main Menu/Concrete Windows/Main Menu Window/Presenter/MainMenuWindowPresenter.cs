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
        private Button _startGameButton;
        private Button _creditsButton;
        private Button _quitButton;

        public void Initialize(IMainMenuWindowModel model, IUIWindowView view, Button startGameButton,
            Button creditsButton, Button quitButton)
        {
            _model = model;
            _view = view;
            _startGameButton = startGameButton;
            _creditsButton = creditsButton;
            _quitButton = quitButton;
            SetInitializedStatus();
        }

        protected override IUIWindowView WindowView => _view;
        protected override IUIWindowModel WindowModel => _model;

        protected override void SubscribeOnWindowEvents()
        {
            _startGameButton.onClick.AddListener(_model.OnStartGameButtonPressed);
            _creditsButton.onClick.AddListener(_model.OnCreditsButtonPressed);
            _quitButton.onClick.AddListener(_model.OnQuitButtonPressed);
        }

        protected override void UnsubscribeFromWindowEvents()
        {
            _startGameButton.onClick.RemoveListener(_model.OnStartGameButtonPressed);
            _creditsButton.onClick.RemoveListener(_model.OnCreditsButtonPressed);
            _quitButton.onClick.RemoveListener(_model.OnQuitButtonPressed);
        }
    }
}