using UI.Window.Model;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Main_Menu_Window.Model
{
    public interface IMainMenuWindowModel : IUIWindowModel
    {
        public void OnStartGameButtonPressed();
        public void OnCreditsButtonPressed();
        public void OnQuitButtonPressed();
    }
}