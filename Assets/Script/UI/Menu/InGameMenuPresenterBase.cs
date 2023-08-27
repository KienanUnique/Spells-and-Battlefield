using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Element.View;
using UI.Menu.Concrete_Types.Game_Over_Menu.Model;
using UI.Window.Model;
using UI.Window.Presenter;
using UnityEngine.UI;

namespace UI.Menu
{
    public abstract class InGameMenuPresenterBase : WindowPresenterBase, IMenuPresenter
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