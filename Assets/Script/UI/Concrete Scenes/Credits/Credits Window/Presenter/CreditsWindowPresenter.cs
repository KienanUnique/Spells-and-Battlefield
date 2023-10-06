using UI.Concrete_Scenes.Credits.Credits_Window.Model;
using UI.Window.Model;
using UI.Window.Presenter;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Credits.Credits_Window.Presenter
{
    public class CreditsWindowPresenter : WindowPresenterBase, IInitializableCreditsWindowPresenter
    {
        private ICreditsWindowModel _model;
        private IUIWindowView _view;
        private Button _quitToMainMenuButton;

        public void Initialize(ICreditsWindowModel model, IUIWindowView view, Button quitToMainMenuButton)
        {
            _view = view;
            _model = model;
            _quitToMainMenuButton = quitToMainMenuButton;
            SetInitializedStatus();
        }

        protected override IUIWindowView WindowView => _view;
        protected override IUIWindowModel WindowModel => _model;

        protected override void SubscribeOnWindowEvents()
        {
            _quitToMainMenuButton.onClick.AddListener(_model.OnQuitToMainMenuButtonPressed);
        }

        protected override void UnsubscribeFromWindowEvents()
        {
            _quitToMainMenuButton.onClick.RemoveListener(_model.OnQuitToMainMenuButtonPressed);
        }
    }
}