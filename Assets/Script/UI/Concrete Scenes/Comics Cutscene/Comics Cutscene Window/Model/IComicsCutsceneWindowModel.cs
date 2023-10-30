using UI.Concrete_Scenes.Comics_Cutscene.Comics_Screen;
using UI.Window.Model;

namespace UI.Concrete_Scenes.Comics_Cutscene.Comics_Cutscene_Window.Model
{
    public interface IComicsCutsceneWindowModel : IUIWindowModel
    {
        public event ComicsCutsceneWindowModel.OnNewScreenOpened NewScreenOpened;
        public IComicsScreen CurrentScreen { get; }
        public bool IsComicsPlaying { get; }
        public void OnAllPanelsShownInCurrentScreen();
        public void SkipPanelAnimation();
    }
}