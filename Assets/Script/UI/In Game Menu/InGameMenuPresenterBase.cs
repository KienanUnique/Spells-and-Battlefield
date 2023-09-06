using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Element.View;
using UI.Window.Model;
using UI.Window.Presenter;
using UnityEngine.UI;

namespace UI.In_Game_Menu
{
    public abstract class InGameMenuPresenterBase : WindowPresenterBase
    {
        private IInGameMenuModelBase _model;
        private IUIElementView _view;
        protected override IUIElementView View => _view;
        protected override IUIWindowModel Model => _model;

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

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
    }
}