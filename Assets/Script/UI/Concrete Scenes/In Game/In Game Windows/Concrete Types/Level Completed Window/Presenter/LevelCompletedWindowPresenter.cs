using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window.Model;
using UI.Window.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window.Presenter
{
    public class LevelCompletedWindowPresenter : InGameWindowPresenterBase,
        IInitializableLevelCompletedWindowPresenter,
        ILevelCompletedWindow
    {
        private Button _loadNextLevel;
        private ILevelCompletedWindowModel _model;

        public void Initialize(IUIWindowView view, ILevelCompletedWindowModel model,
            List<IDisableable> itemsNeedDisabling, Button restartLevelButton, Button goToMainWindowButton,
            Button loadNextLevel)
        {
            _loadNextLevel = loadNextLevel;
            _model = model;
            InitializeBase(view, model, itemsNeedDisabling, restartLevelButton, goToMainWindowButton);
            SetInitializedStatus();
        }

        protected override void SubscribeOnWindowEvents()
        {
            base.SubscribeOnWindowEvents();
            _loadNextLevel.onClick.AddListener(_model.OnLoadNextLevelButtonPressed);
        }

        protected override void UnsubscribeFromWindowEvents()
        {
            base.UnsubscribeFromWindowEvents();
            _loadNextLevel.onClick.RemoveListener(_model.OnLoadNextLevelButtonPressed);
        }
    }
}