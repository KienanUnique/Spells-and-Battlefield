using UI.Window.Model;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Model
{
    public interface IStartGameWindowModel : IUIWindowModel
    {
        void OnBackButtonPressed();
        void OnLoadButtonPressed();
    }
}