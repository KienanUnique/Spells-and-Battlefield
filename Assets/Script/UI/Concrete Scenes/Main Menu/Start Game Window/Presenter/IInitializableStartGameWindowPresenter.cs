using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Model;
using UI.Element.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Presenter
{
    public interface IInitializableStartGameWindowPresenter
    {
        void Initialize(IStartGameWindowModel model, IUIElementView view, Button backButton, Button loadButton);
    }
}