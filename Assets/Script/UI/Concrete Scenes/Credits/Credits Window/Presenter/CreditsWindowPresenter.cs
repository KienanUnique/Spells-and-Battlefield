using UI.Concrete_Scenes.Credits.Credits_Window.Model;
using UI.Element.View;
using UI.Window.Model;
using UI.Window.Presenter;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Credits.Credits_Window.Presenter
{
    public class CreditsWindowPresenter : WindowPresenterBase, IInitializableCreditsWindowPresenter
    {
        private ICreditsWindowModel _model;
        private IUIElementView _view;

        public void Initialize(ICreditsWindowModel model, IUIElementView view, Button quitToMainMenuButton)
        {
            _view = view;
            _model = model;
            quitToMainMenuButton.onClick.AddListener(_model.OnQuitToMainMenuButtonPressed);
            SetInitializedStatus();
        }

        protected override IUIElementView View => _view;

        protected override IUIWindowModel Model => _model;
    }
}