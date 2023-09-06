using UI.Gameplay_UI;
using UI.In_Game_Menu.Concrete_Types.Game_Over_Menu;
using UI.In_Game_Menu.Concrete_Types.Level_Completed_Menu;
using UI.In_Game_Menu.Concrete_Types.Pause_Menu;
using UI.Managers.UI_Windows_Stack_Manager;

namespace UI.Managers.Concrete_Types.In_Game.Setup
{
    public interface IInitializableInGameManagerUI
    {
        public void Initialize(IGameplayUI gameplayUI, IGameOverMenu gameOverMenu, IPauseMenu pauseMenu,
            ILevelCompletedMenu levelCompletedMenu, IUIWindowsStackManager windowsManager);
    }
}