using UI.Loading_Window.Model;
using UI.Loading_Window.View;
using UI.Window.Model;
using UI.Window.Presenter;
using UI.Window.View;

namespace UI.Loading_Window.Presenter
{
    public class LoadingWindowPresenter : WindowPresenterBase, IInitializableLoadingWindowPresenter, ILoadingWindow
    {
        private ILoadingWindowModel _windowModel;
        private ILoadingWindowView _view;

        public void Initialize(ILoadingWindowModel model, ILoadingWindowView view)
        {
            _windowModel = model;
            _view = view;
            SetInitializedStatus();
        }

        protected override IUIWindowView WindowView => _view;
        protected override IUIWindowModel WindowModel => _windowModel;

        protected override void SubscribeOnWindowEvents()
        {
        }

        protected override void UnsubscribeFromWindowEvents()
        {
        }
    }
}