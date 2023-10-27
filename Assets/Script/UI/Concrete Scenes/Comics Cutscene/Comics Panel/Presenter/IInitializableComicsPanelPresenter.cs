using UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.View;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Panel.Presenter
{
    public interface IInitializableComicsPanelPresenter
    {
        void Initialize(IComicsPanelView view);
    }
}