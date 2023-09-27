using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window.Model;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window.Presenter
{
    public interface IInitializableLevelCompletedWindowPresenter
    {
        public void Initialize(IUIWindowView view, ILevelCompletedWindowModel model,
            List<IDisableable> itemsNeedDisabling, Button restartLevelButton, Button goToMainWindowButton,
            Button loadNextLevel);
    }
}