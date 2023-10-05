using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Model;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Presenter
{
    public interface IInitializableStartGameWindowPresenter
    {
        void Initialize(IStartGameWindowModel model, IUIWindowView view, Button backButton, Button loadButton);
    }
}