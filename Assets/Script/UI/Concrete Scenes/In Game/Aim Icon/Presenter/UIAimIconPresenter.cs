using UI.Element.Presenter;
using UI.Element.View;

namespace UI.Concrete_Scenes.In_Game.Aim_Icon.Presenter
{
    public class UIAimIconPresenter : UIElementPresenterBase, IInitializableUIAimIconPresenter
    {
        private IUIElementView _view;

        public void Initialize(IUIElementView view)
        {
            _view = view;
            SetInitializedStatus();
        }

        protected override IUIElementView View => _view;
    }
}