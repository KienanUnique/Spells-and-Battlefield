using DG.Tweening;

namespace Systems.In_Game_Systems.Post_Processing_Controller.Settings
{
    public interface IPostProcessingControllerSettings
    {
        public Ease ApplyEffectsVolumeEase { get; }
        public float ApplyDashEffectsVolumeDurationSeconds { get; }
        public float ApplyDashAimingEffectsVolumeDurationSeconds { get; }
        public float ApplyDialogEffectsVolumeDurationSeconds { get; }
    }
}