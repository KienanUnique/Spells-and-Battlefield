using UI.Concrete_Scenes.Credits.Credits_Window.Model;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Credits.Credits_Window.Presenter
{
    public interface IInitializableCreditsWindowPresenter
    {
        public void Initialize(ICreditsWindowModel model, IUIWindowView view, Button quitToMainMenuButton);
    }
}