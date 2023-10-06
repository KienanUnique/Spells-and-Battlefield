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
        private Button _restartLevelButton;
        private Button _goToMainWindowButton;
        protected override IUIWindowView WindowView => _view;
        protected override IUIWindowModel WindowModel => _model;

        protected void InitializeBase(IUIWindowView view, IInGameWindowModelBase modelBase,
            List<IDisableable> itemsNeedDisabling, Button restartLevelButton, Button goToMainWindowButton)
        {
            _view = view;
            _model = modelBase;
            _restartLevelButton = restartLevelButton;
            _goToMainWindowButton = goToMainWindowButton;
            SetItemsNeedDisabling(itemsNeedDisabling);
            SetInitializedStatus();
        }

        protected override void SubscribeOnWindowEvents()
        {
            _goToMainWindowButton.onClick.AddListener(_model.OnQuitMainWindowButtonPressed);
            _restartLevelButton.onClick.AddListener(_model.OnRestartLevelWindowButtonPressed);
        }

        protected override void UnsubscribeFromWindowEvents()
        {
            _goToMainWindowButton.onClick.RemoveListener(_model.OnQuitMainWindowButtonPressed);
            _restartLevelButton.onClick.RemoveListener(_model.OnRestartLevelWindowButtonPressed);
        }
    }
}