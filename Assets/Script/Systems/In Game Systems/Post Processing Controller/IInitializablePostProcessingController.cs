using Player;
using Systems.Dialog;
using Systems.In_Game_Systems.Post_Processing_Controller.Settings;
using UnityEngine.Rendering;

namespace Systems.In_Game_Systems.Post_Processing_Controller
{
    public interface IInitializablePostProcessingController
    {
        void Initialize(IPlayerInformationProvider playerInformationProvider, Volume dashEffectsVolume,
            Volume dashAimingEffectsVolume, Volume dialogEffectsVolume, IPostProcessingControllerSettings settings,
            IDialogService dialogService);
    }
}