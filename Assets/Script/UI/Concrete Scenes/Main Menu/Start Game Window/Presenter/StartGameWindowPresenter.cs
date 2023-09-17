using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Model;
using UI.Element.View;
using UI.Window.Model;
using UI.Window.Presenter;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Presenter
{
    public class StartGameWindowPresenter : WindowPresenterBase, IStartGameWindow, IInitializableStartGameWindowPresenter
    {
        private IStartGameWindowModel _model;
        private IUIElementView _view;

        public void Initialize(IStartGameWindowModel model, IUIElementView view, Button backButton, Button loadButton)
        {
            _model = model;
            _view = view;
            backButton.onClick.AddListener(model.OnBackButtonPressed);
            loadButton.onClick.AddListener(model.OnLoadButtonPressed);
            SetInitializedStatus();
        }

        protected override IUIElementView View => _view;
        protected override IUIWindowModel Model => _model;
    }
}