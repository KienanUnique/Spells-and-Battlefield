using UI.Window.Model;

namespace UI.Menu.Concrete_Types.Game_Over_Menu.Model
{
    public interface IInGameMenuModelBase : IUIWindowModel
    {
        public void OnQuitMainMenuButtonPressed();
        public void OnRestartLevelMenuButtonPressed();
    }
}