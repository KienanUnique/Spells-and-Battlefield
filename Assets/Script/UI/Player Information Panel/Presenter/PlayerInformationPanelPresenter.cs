using UI.Element.Presenter;
using UI.Element.View;

namespace UI.Player_Information_Panel.Presenter
{
    public class PlayerInformationPanelPresenter : UIElementPresenterBase, IInitializablePlayerInformationPanelPresenter
    {
        private IUIElementView _view;
        protected override IUIElementView View => _view;

        public void Initialize(IUIElementView view)
        {
            _view = view;
            SetInitializedStatus();
        }
    }
}