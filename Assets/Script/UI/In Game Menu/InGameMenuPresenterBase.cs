using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Element.View;
using UI.Menu;
using UI.Window.Model;
using UI.Window.Presenter;
using UnityEngine.UI;

namespace UI.In_Game_Menu
{
    public abstract class InGameMenuPresenterBase : WindowPresenterBase
    {
        private IUIElementView _view;
        private IInGameMenuModelBase _model;
        protected override IUIElementView View => _view;
        protected override IUIWindowModel Model => _model;

        protected void InitializeBase(IUIElementView view, IInGameMenuModelBase modelBase,
            List<IDisableable> itemsNeedDisabling, Button restartLevelButton, Button goToMainMenuButton)
        {
            _view = view;
            _model = modelBase;
            goToMainMenuButton.onClick.AddListener(modelBase.OnQuitMainMenuButtonPressed);
            restartLevelButton.onClick.AddListener(modelBase.OnRestartLevelMenuButtonPressed);
            SetItemsNeedDisabling(itemsNeedDisabling);
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