using System;
using UI.Gameplay_UI;
using UI.In_Game_Menu.Concrete_Types.Game_Over_Menu;
using UI.In_Game_Menu.Concrete_Types.Level_Completed_Menu;
using UI.In_Game_Menu.Concrete_Types.Pause_Menu;
using UI.Managers.Concrete_Types.In_Game.Setup;
using UI.Managers.UI_Windows_Stack_Manager;
using UI.Window;

namespace UI.Managers.Concrete_Types.In_Game
{
    public class InGameManagerUI : ManagerUIBase, IInGameManagerUI, IInitializableInGameManagerUI
    {
        private IGameOverMenu _gameOverMenu;
        private IGameplayUI _gameplayUI;
        private ILevelCompletedMenu _levelCompletedMenu;
        private IPauseMenu _pauseMenu;
        private IUIWindowsStackManager _windowsManager;

        public void Initialize(IGameplayUI gameplayUI, IGameOverMenu gameOverMenu, IPauseMenu pauseMenu,
            ILevelCompletedMenu levelCompletedMenu, IUIWindowsStackManager windowsManager)
        {
            _gameplayUI = gameplayUI;
            _gameOverMenu = gameOverMenu;
            _pauseMenu = pauseMenu;
            _levelCompletedMenu = levelCompletedMenu;
            _windowsManager = windowsManager;
            SetInitializedStatus();
        }

        public event Action AllMenusClosed;

        protected override IUIWindowsStackManager WindowsManager => _windowsManager;

        public void SwitchTo(InGameUIElementsGroup needElementsGroup)
        {
            IUIWindow elementToOpen = needElementsGroup switch
            {
                InGameUIElementsGroup.GameOverMenu => _gameOverMenu,
                InGameUIElementsGroup.LevelCompletedMenu => _levelCompletedMenu,
                InGameUIElementsGroup.PauseMenu => _pauseMenu,
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
                AllMenusClosed?.Invoke();
            }
        }
    }
}