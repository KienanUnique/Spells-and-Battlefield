using DG.Tweening;

namespace Systems.In_Game_Systems.Post_Processing_Controller.Settings
{
    public interface IPostProcessingControllerSettings
    {
        Ease ApplyDashEffectsVolumeEase { get; }
        float ApplyDashEffectsVolumeDurationSeconds { get; }
    }
}