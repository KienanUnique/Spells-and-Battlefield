using UI.Loading_Window.Model;
using UI.Loading_Window.View;

namespace UI.Loading_Window.Presenter
{
    public interface IInitializableLoadingWindowPresenter
    {
        void Initialize(ILoadingWindowModel model, ILoadingWindowView view);
    }
}