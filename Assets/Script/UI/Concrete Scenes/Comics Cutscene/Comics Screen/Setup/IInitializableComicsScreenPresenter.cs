using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Model;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen.Setup
{
    public interface IInitializableComicsScreenPresenter
    {
        public void Initialize(IComicsScreenModel model);
    }
}