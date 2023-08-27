using UI.Gameplay_UI;
using UI.Loading_Window;
using UI.Managers.UI_Windows_Stack_Manager;
using UI.Menu.Concrete_Types.Game_Over_Menu;
using UI.Menu.Concrete_Types.Level_Completed_Menu;
using UI.Menu.Concrete_Types.Pause_Menu;

namespace UI.Managers.In_Game.Setup
{
    public interface IInitializableInGameManagerUI
    {
        void Initialize(ILoadingWindow loadingWindow, IGameplayUI gameplayUI, IGameOverMenu gameOverMenu,
            IPauseMenu pauseMenu, ILevelCompletedMenu levelCompletedMenu, IUIWindowsStackManager windowsManager);
    }
}