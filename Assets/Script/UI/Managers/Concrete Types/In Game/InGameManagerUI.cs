using System;
using UI.Concrete_Scenes.In_Game.Gameplay_UI;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Game_Over_Window;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window;
using UI.Managers.Concrete_Types.In_Game.Setup;
using UI.Managers.UI_Windows_Stack_Manager;
using UI.Window;

namespace UI.Managers.Concrete_Types.In_Game
{
    public class InGameManagerUI : ManagerUIBase, IInGameManagerUI, IInitializableInGameManagerUI
    {
        private IGameOverWindow _gameOverWindow;
        private IGameplayUI _gameplayUI;
        private ILevelCompletedWindow _levelCompletedWindow;
        private IPauseWindow _pauseWindow;
        private IUIWindowsStackManager _windowsManager;

        public void Initialize(IGameplayUI gameplayUI, IGameOverWindow gameOverWindow, IPauseWindow pauseWindow,
            ILevelCompletedWindow levelCompletedWindow, IUIWindowsStackManager windowsManager)
        {
            _gameplayUI = gameplayUI;
            _gameOverWindow = gameOverWindow;
            _pauseWindow = pauseWindow;
            _levelCompletedWindow = levelCompletedWindow;
            _windowsManager = windowsManager;
            SetInitializedStatus();
        }

        public event Action AllWindowsClosed;

        protected override IUIWindowsStackManager WindowsManager => _windowsManager;

        public void SwitchTo(InGameUIElementsGroup needElementsGroup)
        {
            IUIWindow elementToOpen = needElementsGroup switch
            {
                InGameUIElementsGroup.GameOverWindow => _gameOverWindow,
                InGameUIElementsGroup.LevelCompletedWindow => _levelCompletedWindow,
                InGameUIElementsGroup.PauseWindow => _pauseWindow,
                _ => throw new ArgumentOutOfRangeException(nameof(needElementsGroup), needElementsGroup, null)
            };
            _windowsManager.Open(elementToOpen);
        }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _windowsManager.WindowClosed += OnWindowClosed;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            _windowsManager.WindowClosed -= OnWindowClosed;
        }

        private void OnWindowClosed()
        {
            if (_windowsManager.CurrentOpenedWindow == _gameplayUI)
            {
                AllWindowsClosed?.Invoke();
            }
        }
    }
}