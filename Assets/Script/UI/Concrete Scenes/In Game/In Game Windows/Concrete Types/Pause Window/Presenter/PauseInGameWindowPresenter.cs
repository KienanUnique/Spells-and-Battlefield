using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window.Model;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window.Presenter
{
    public class PauseInGameWindowPresenter : InGameWindowPresenterBase,
        IInitializablePauseWindowPresenter,
        IPauseWindow
    {
        public void Initialize(IUIWindowView view, IPauseWindowModel model, List<IDisableable> itemsNeedDisabling,
            Button restartLevelButton, Button goToMainWindowButton, Button continueGameButton)
        {
            continueGameButton.onClick.AddListener(model.OnContinueGameButtonPressed);
            InitializeBase(view, model, itemsNeedDisabling, restartLevelButton, goToMainWindowButton);
            SetInitializedStatus();
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}