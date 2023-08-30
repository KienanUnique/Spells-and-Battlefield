using Interfaces;
using Systems.In_Game_Systems.Level_Finish_Zone;
using Systems.In_Game_Systems.Time_Controller;
using Systems.Input_Manager;
using Systems.Scene_Switcher;
using UI.Managers.In_Game;

namespace Systems.In_Game_Systems.Game_Controller
{
    public interface IInitializableGameController
    {
        public void Initialize(IInGameManagerUI inGameManagerUI, IPlayerInformationProvider playerInformationProvider,
            IInGameSystemInputManager inGameSystemInput, ITimeController timeController,
            ILevelFinishZone levelFinishZone, IInGameSceneSwitcher inGameSceneSwitcher);
    }
}