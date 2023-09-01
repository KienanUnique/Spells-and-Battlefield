using UI.Window.Model;

namespace UI.In_Game_Menu
{
    public interface IInGameMenuModelBase : IUIWindowModel
    {
        public void OnQuitMainMenuButtonPressed();
        public void OnRestartLevelMenuButtonPressed();
    }
}