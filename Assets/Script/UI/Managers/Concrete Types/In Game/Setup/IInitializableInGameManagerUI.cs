using Systems.Input_Manager;
using Systems.Scenes_Controller.Concrete_Types;
using UI.Concrete_Scenes.In_Game.Gameplay_UI;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Dialog_Window;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Game_Over_Window;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window;
using UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window;
using UI.Loading_Window;
using UI.Managers.UI_Windows_Stack_Manager;

namespace UI.Managers.Concrete_Types.In_Game.Setup
{
    public interface IInitializableInGameManagerUI
    {
        public void Initialize(IInputManagerForUI inputManagerForUI, IGameplayUI gameplayUI,
            IGameOverWindow gameOverWindow, IPauseWindow pauseWindow, ILevelCompletedWindow levelCompletedWindow,
            IScenesController scenesController, ILoadingWindow loadingWindow, IDialogWindow dialogWindow,
            IUIWindowsStackManager windowsManager);
    }
}