using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window.Model;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window.Presenter
{
    public interface IInitializablePauseWindowPresenter
    {
        public void Initialize(IUIWindowView view, IPauseWindowModel model, List<IDisableable> itemsNeedDisabling,
            Button restartLevelButton, Button goToMainWindowButton, Button continueGameButton);
    }
}