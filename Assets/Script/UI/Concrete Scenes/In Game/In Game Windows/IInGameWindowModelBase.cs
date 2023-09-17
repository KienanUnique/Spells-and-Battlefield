using UI.Window.Model;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows
{
    public interface IInGameWindowModelBase : IUIWindowModel
    {
        public void OnQuitMainWindowButtonPressed();
        public void OnRestartLevelWindowButtonPressed();
    }
}