using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using UI.Gameplay_UI;
using UI.Loading_Window;
using UI.Managers.In_Game.Setup;
using UI.Managers.UI_Windows_Stack_Manager;
using UI.Menu.Concrete_Types.Game_Over_Menu;
using UI.Menu.Concrete_Types.Level_Completed_Menu;
using UI.Menu.Concrete_Types.Pause_Menu;
using UI.Window;

namespace UI.Managers.In_Game
{
    public class InGameManagerUI : InitializableMonoBehaviourBase, IInGameUIControllable, IInGameManagerUI,
        IInitializableInGameManagerUI, IInGameManagerUIInitializationStatus

    {
    private ILoadingWindow _loadingWindow;
    private IGameplayUI _gameplayUI;
    private IGameOverMenu _gameOverMenu;
    private IPauseMenu _pauseMenu;
    private ILevelCompletedMenu _levelCompletedMenu;
    private IUIWindowsStackManager _windowsManager;

    public void Initialize(ILoadingWindow loadingWindow, IGameplayUI gameplayUI, IGameOverMenu gameOverMenu,
        IPauseMenu pauseMenu, ILevelCompletedMenu levelCompletedMenu, IUIWindowsStackManager windowsManager)
    {
        _loadingWindow = loadingWindow;
        _gameplayUI = gameplayUI;
        _gameOverMenu = gameOverMenu;
        _pauseMenu = pauseMenu;
        _levelCompletedMenu = levelCompletedMenu;
        _windowsManager = windowsManager;
        SetInitializedStatus();
    }

    public event Action RestartLevelRequested;
    public event Action QuitToMainMenuRequested;
    public event Action LoadNextLevelRequested;
    public event Action AllMenusClosed;

    public void SwitchTo(InGameUIElementsGroup needElementsGroup)
    {
        IUIWindow elementToOpen = needElementsGroup switch
        {
            InGameUIElementsGroup.GameOverMenu => _gameOverMenu,
            InGameUIElementsGroup.LevelCompletedMenu => _levelCompletedMenu,
            InGameUIElementsGroup.PauseMenu => _pauseMenu,
            InGameUIElementsGroup.LoadingWindow => _loadingWindow,
            _ => throw new ArgumentOutOfRangeException(nameof(needElementsGroup), needElementsGroup, null)
        };
        _windowsManager.Open(elementToOpen);
    }

    public void RequestQuitToMainMenu()
    {
        QuitToMainMenuRequested?.Invoke();
    }

    public void RequestRestartLevel()
    {
        RestartLevelRequested?.Invoke();
    }

    public void RequestLoadNextLevel()
    {
        LoadNextLevelRequested?.Invoke();
    }

    public void TryCloseCurrentWindow()
    {
        _windowsManager.TryCloseCurrentElement();
    }

    protected override void SubscribeOnEvents()
    {
        _windowsManager.WindowClosed += OnWindowClosed;
    }

    protected override void UnsubscribeFromEvents()
    {
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