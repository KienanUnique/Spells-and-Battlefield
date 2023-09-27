using UI.Concrete_Scenes.Main_Menu.Main_Menu_Window.Model;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Main_Menu.Main_Menu_Window.Presenter
{
    public interface IInitializableMainMenuWindowPresenter
    {
        public void Initialize(IMainMenuWindowModel model, IUIWindowView view, Button startGameButton,
            Button creditsButton, Button quitButton);
    }
}