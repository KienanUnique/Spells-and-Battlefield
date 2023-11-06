using DG.Tweening;

namespace Player.Camera_Effects.Camera_Field_Of_View_Calculator
{
    public interface IPlayerCameraFOVSettings
    {
        public float BaseFOV { get; }
        public float DashFOV { get; }
        public float MaximumOverSpeedFieldOfViewAdditionalValue { get; }
        public float OnDashFOVChangeAnimationDuration { get; }
        public float OverSpeedFOVChangeSpeed { get; }
        public Ease ChangeCameraFOVAnimationEase { get; }
        public float MaximumOverSpeedValueForFieldOfView { get; }
    }
}