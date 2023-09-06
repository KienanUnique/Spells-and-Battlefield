using UI.Element.View;
using UI.Loading_Window.Model;
using UI.Loading_Window.View;
using UI.Window.Model;
using UI.Window.Presenter;

namespace UI.Loading_Window.Presenter
{
    public class LoadingWindowPresenter : WindowPresenterBase, IInitializableLoadingWindowPresenter, ILoadingWindow
    {
        private ILoadingWindowModel _model;
        private ILoadingWindowView _view;

        public void Initialize(ILoadingWindowModel model, ILoadingWindowView view)
        {
            _model = model;
            _view = view;
            SetInitializedStatus();
        }

        protected override IUIElementView View => _view;
        protected override IUIWindowModel Model => _model;
    }
}