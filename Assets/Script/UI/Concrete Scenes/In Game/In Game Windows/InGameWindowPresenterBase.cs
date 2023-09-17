using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Element.View;
using UI.Window.Model;
using UI.Window.Presenter;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows
{
    public abstract class InGameWindowPresenterBase : WindowPresenterBase
    {
        private IInGameWindowModelBase _model;
        private IUIElementView _view;
        protected override IUIElementView View => _view;
        protected override IUIWindowModel Model => _model;

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

        protected void InitializeBase(IUIElementView view, IInGameWindowModelBase modelBase,
            List<IDisableable> itemsNeedDisabling, Button restartLevelButton, Button goToMainWindowButton)
        {
            _view = view;
            _model = modelBase;
            goToMainWindowButton.onClick.AddListener(modelBase.OnQuitMainWindowButtonPressed);
            restartLevelButton.onClick.AddListener(modelBase.OnRestartLevelWindowButtonPressed);
            SetItemsNeedDisabling(itemsNeedDisabling);
            SetInitializedStatus();
        }
    }
}