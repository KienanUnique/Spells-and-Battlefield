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
        private Button _continueGameButton;
        private IPauseWindowModel _model;

        public void Initialize(IUIWindowView view, IPauseWindowModel model, List<IDisableable> itemsNeedDisabling,
            Button restartLevelButton, Button goToMainWindowButton, Button continueGameButton)
        {
            _model = model;
            _continueGameButton = continueGameButton;
            InitializeBase(view, model, itemsNeedDisabling, restartLevelButton, goToMainWindowButton);
            SetInitializedStatus();
        }

        protected override void SubscribeOnWindowEvents()
        {
            base.SubscribeOnWindowEvents();
            _continueGameButton.onClick.AddListener(_model.OnContinueGameButtonPressed);
        }

        protected override void UnsubscribeFromWindowEvents()
        {
            base.UnsubscribeFromWindowEvents();
            _continueGameButton.onClick.RemoveListener(_model.OnContinueGameButtonPressed);
        }
    }
}