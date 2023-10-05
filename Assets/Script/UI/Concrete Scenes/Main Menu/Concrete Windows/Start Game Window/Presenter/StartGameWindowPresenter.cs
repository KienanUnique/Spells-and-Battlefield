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

        public void Initialize(IStartGameWindowModel model, IUIWindowView view, Button backButton, Button loadButton)
        {
            _model = model;
            _view = view;
            backButton.onClick.AddListener(model.OnBackButtonPressed);
            loadButton.onClick.AddListener(model.OnLoadButtonPressed);
            SetInitializedStatus();
        }

        protected override IUIWindowView WindowView => _view;
        protected override IUIWindowModel WindowModel => _model;
    }
}