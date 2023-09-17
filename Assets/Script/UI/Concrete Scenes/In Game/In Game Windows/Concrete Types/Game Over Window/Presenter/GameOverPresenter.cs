using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Game_Over_Window.Model;
using UI.Element.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Game_Over_Window.Presenter
{
    public class GameOverPresenter : InGameWindowPresenterBase, IInitializableGameOverPresenter, IGameOverWindow
    {
        public void Initialize(IUIElementView view, IGameOverWindowModel model, List<IDisableable> itemsNeedDisabling,
            Button restartLevelButton, Button goToMainWindowButton)
        {
            InitializeBase(view, model, itemsNeedDisabling, restartLevelButton, goToMainWindowButton);
            SetInitializedStatus();
        }
    }
}