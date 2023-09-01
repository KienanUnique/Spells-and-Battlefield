using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Element.View;
using UI.In_Game_Menu.Concrete_Types.Pause_Menu.Model;
using UnityEngine.UI;

namespace UI.Menu.Concrete_Types.Pause_Menu.Presenter
{
    public interface IInitializablePauseMenuPresenter
    {
        public void Initialize(IUIElementView view, IPauseMenuModel model, List<IDisableable> itemsNeedDisabling,
            Button restartLevelButton, Button goToMainMenuButton, Button continueGameButton);
    }
}