using Interfaces;
using Systems.In_Game_Systems.Post_Processing_Controller.Settings;
using UnityEngine.Rendering;

namespace Systems.In_Game_Systems.Post_Processing_Controller
{
    public interface IInitializablePostProcessingController
    {
        void Initialize(IPlayerInformationProvider playerInformationProvider, Volume dashEffectsVolume,
            IPostProcessingControllerSettings settings);
    }
}