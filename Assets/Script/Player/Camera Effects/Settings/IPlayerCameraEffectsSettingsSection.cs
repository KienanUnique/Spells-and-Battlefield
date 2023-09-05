using DG.Tweening;

namespace Player.Camera_Effects.Settings
{
    public interface IPlayerCameraEffectsSettings
    {
        float RotationAngle { get; }
        float RotateDuration { get; }
        float CameraIncreasedFOV { get; }
        float CameraNormalFOV { get; }
        float ChangeCameraFOVAnimationDuration { get; }
        Ease ChangeCameraFOVAnimationEase { get; }
    }
}