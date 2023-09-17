using UI.Concrete_Scenes.In_Game.Gameplay_UI.Model;

namespace UI.Concrete_Scenes.In_Game.Gameplay_UI.Presenter
{
    public interface IInitializableGameplayUIPresenter
    {
        void Initialize(IGameplayUIModel model);
    }
}