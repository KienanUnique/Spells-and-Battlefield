using UI.Concrete_Scenes.Credits.Credits_Window.Model;
using UI.Element.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Credits.Credits_Window.Presenter
{
    public interface IInitializableCreditsWindowPresenter
    {
        public void Initialize(ICreditsWindowModel model, IUIElementView view, Button quitToMainMenuButton);
    }
}