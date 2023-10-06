using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Model;
using UI.Window.Model;
using UI.Window.Presenter;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Presenter
{
    public class StartGameWindowPresenter : WindowPresenterBase,
        IStartGameWindow,
        IInitializableStartGameWindowPresenter
    {
        private IStartGameWindowModel _model;
        private IUIWindowView _view;
        private Button _backButton;
        private Button _loadButton;

        public void Initialize(IStartGameWindowModel model, IUIWindowView view, Button backButton, Button loadButton)
        {
            _model = model;
            _view = view;
            _backButton = backButton;
            _loadButton = loadButton;
            SetInitializedStatus();
        }

        protected override IUIWindowView WindowView => _view;
        protected override IUIWindowModel WindowModel => _model;

        protected override void SubscribeOnWindowEvents()
        {
            _backButton.onClick.AddListener(_model.OnBackButtonPressed);
            _loadButton.onClick.AddListener(_model.OnLoadButtonPressed);
        }

        protected override void UnsubscribeFromWindowEvents()
        {
            _backButton.onClick.RemoveListener(_model.OnBackButtonPressed);
            _loadButton.onClick.RemoveListener(_model.OnLoadButtonPressed);
        }
    }
}