using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Element.View;
using UI.In_Game_Menu;
using UI.In_Game_Menu.Concrete_Types.Level_Completed_Menu;
using UI.In_Game_Menu.Concrete_Types.Level_Completed_Menu.Model;
using UI.In_Game_Menu.Concrete_Types.Level_Completed_Menu.Presenter;
using UnityEngine.UI;

namespace UI.Menu.Concrete_Types.Level_Completed_Menu.Presenter
{
    public class LevelCompletedMenuPresenter : InGameMenuPresenterBase,
        IInitializableLevelCompletedMenuPresenter,
        ILevelCompletedMenu
    {
        public void Initialize(IUIElementView view, ILevelCompletedMenuModel model,
            List<IDisableable> itemsNeedDisabling, Button restartLevelButton, Button goToMainMenuButton,
            Button loadNextLevel)
        {
            loadNextLevel.onClick.AddListener(model.OnLoadNextLevelButtonPressed);
            InitializeBase(view, model, itemsNeedDisabling, restartLevelButton, goToMainMenuButton);
            SetInitializedStatus();
        }
    }
}