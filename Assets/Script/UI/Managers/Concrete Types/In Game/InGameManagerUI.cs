using System;
using Systems.Input_Manager;
using Systems.Scene_Switcher.Concrete_Types;
using UI.Concrete_Scenes.In_Game.Gameplay_UI;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Game_Over_Window;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window;
using UI.Loading_Window;
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
        private IScenesController _scenesController;
        private ILoadingWindow _loadingWindow;
        private IInputManagerForUI _inputManagerForUI;

        public void Initialize(IInputManagerForUI inputManagerForUI, IGameplayUI gameplayUI,
            IGameOverWindow gameOverWindow, IPauseWindow pauseWindow, ILevelCompletedWindow levelCompletedWindow,
            IScenesController scenesController, ILoadingWindow loadingWindow, IUIWindowsStackManager windowsManager)
        {
            _inputManagerForUI = inputManagerForUI;
            _gameplayUI = gameplayUI;
            _gameOverWindow = gameOverWindow;
            _pauseWindow = pauseWindow;
            _levelCompletedWindow = levelCompletedWindow;
            _windowsManager = windowsManager;
            _scenesController = scenesController;
            _loadingWindow = loadingWindow;
            SetInitializedStatus();
        }

        public event Action AllWindowsClosed;

        protected override ILoadingWindow LoadingWindow => _loadingWindow;
        protected override IUIWindowsStackManager WindowsManager => _windowsManager;
        protected override IScenesController ScenesController => _scenesController;
        protected override IInputManagerForUI InputManagerForUI => _inputManagerForUI;

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