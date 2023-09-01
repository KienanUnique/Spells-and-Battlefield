using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Element.View;
using UI.In_Game_Menu.Concrete_Types.Game_Over_Menu.Model;
using UnityEngine.UI;

namespace UI.In_Game_Menu.Concrete_Types.Game_Over_Menu.Presenter
{
    public class GameOverPresenter : InGameMenuPresenterBase, IInitializableGameOverPresenter, IGameOverMenu
    {
        public void Initialize(IUIElementView view, IGameOverMenuModel model, List<IDisableable> itemsNeedDisabling,
            Button restartLevelButton, Button goToMainMenuButton)
        {
            InitializeBase(view, model, itemsNeedDisabling, restartLevelButton, goToMainMenuButton);
            SetInitializedStatus();
        }
    }
}