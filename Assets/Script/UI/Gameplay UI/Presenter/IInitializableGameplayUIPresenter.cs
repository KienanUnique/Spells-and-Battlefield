using UI.Gameplay_UI.Model;

namespace UI.Gameplay_UI.Presenter
{
    public interface IInitializableGameplayUIPresenter
    {
        void Initialize(IGameplayUIModel model);
    }
}