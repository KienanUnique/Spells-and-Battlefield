using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Window.Model;
using UI.Window.Presenter;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows
{
    public abstract class InGameWindowPresenterBase : WindowPresenterBase
    {
        private IInGameWindowModelBase _model;
        private IUIWindowView _view;
        protected override IUIWindowView WindowView => _view;
        protected override IUIWindowModel WindowModel => _model;

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }

        protected void InitializeBase(IUIWindowView view, IInGameWindowModelBase modelBase,
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