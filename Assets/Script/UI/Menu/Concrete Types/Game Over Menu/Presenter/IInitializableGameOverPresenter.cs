using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Element.View;
using UI.Menu.Concrete_Types.Game_Over_Menu.Model;
using UnityEngine.UI;

namespace UI.Menu.Concrete_Types.Game_Over_Menu.Presenter
{
    public interface IInitializableGameOverPresenter
    {
        void Initialize(IUIElementView view, IGameOverMenuModel model, List<IDisableable> itemsNeedDisabling,
            Button restartLevelButton, Button goToMainMenuButton);
    }
}