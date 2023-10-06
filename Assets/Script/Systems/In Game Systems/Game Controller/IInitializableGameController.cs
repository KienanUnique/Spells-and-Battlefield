using Player;
using Systems.In_Game_Systems.Level_Finish_Zone;
using Systems.In_Game_Systems.Time_Controller;
using Systems.Input_Manager.Concrete_Types.In_Game;
using UI.Managers.Concrete_Types.In_Game;

namespace Systems.In_Game_Systems.Game_Controller
{
    public interface IInitializableGameController
    {
        public void Initialize(IInGameManagerUI inGameManagerUI, IPlayerInformationProvider playerInformationProvider,
            IInGameSystemInputManager inGameSystemInput, ITimeController timeController,
            ILevelFinishZone levelFinishZone);
    }
}