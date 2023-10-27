using Systems.Input_Manager.Concrete_Types.Comics_Cutscene;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Cutscene_Window.Model;
using UI.Window.View;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Cutscene_Window.Setup
{
    public interface IInitializableComicsCutsceneWindowPresenter
    {
        public void Initialize(IComicsCutsceneWindowModel model, IUIWindowView view,
            IComicsCutsceneInputManager inputManager);
    }
}