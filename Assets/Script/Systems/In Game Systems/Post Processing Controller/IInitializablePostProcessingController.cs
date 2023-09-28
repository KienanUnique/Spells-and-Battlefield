using Player;
using Systems.In_Game_Systems.Post_Processing_Controller.Settings;
using UnityEngine.Rendering;

namespace Systems.In_Game_Systems.Post_Processing_Controller
{
    public interface IInitializablePostProcessingController
    {
        public void Initialize(IPlayerInformationProvider playerInformationProvider, Volume dashEffectsVolume,
            Volume dashAimingEffectsVolume, IPostProcessingControllerSettings settings);
    }
}