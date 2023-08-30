using Interfaces;
using Systems.In_Game_Systems.Time_Controller.Settings;

namespace Systems.In_Game_Systems.Time_Controller
{
    public interface IInitializableTimeController
    {
        void Initialize(IPlayerInformationProvider playerInformationProvider, ITimeControllerSettings settings);
    }
}