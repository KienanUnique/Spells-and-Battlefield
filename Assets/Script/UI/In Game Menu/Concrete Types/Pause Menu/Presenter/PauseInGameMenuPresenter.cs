using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Element.View;
using UI.In_Game_Menu.Concrete_Types.Pause_Menu.Model;
using UI.Menu.Concrete_Types.Pause_Menu.Presenter;
using UnityEngine.UI;

namespace UI.In_Game_Menu.Concrete_Types.Pause_Menu.Presenter
{
    public class PauseInGameMenuPresenter : InGameMenuPresenterBase, IInitializablePauseMenuPresenter, IPauseMenu
    {
        public void Initialize(IUIElementView view, IPauseMenuModel model, List<IDisableable> itemsNeedDisabling,
            Button restartLevelButton, Button goToMainMenuButton, Button continueGameButton)
        {
            continueGameButton.onClick.AddListener(model.OnContinueGameButtonPressed);
            InitializeBase(view, model, itemsNeedDisabling, restartLevelButton, goToMainMenuButton);
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