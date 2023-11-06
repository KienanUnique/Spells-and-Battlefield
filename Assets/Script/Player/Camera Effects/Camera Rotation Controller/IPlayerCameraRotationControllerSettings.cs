using DG.Tweening;

namespace Player.Camera_Effects.Camera_Rotator
{
    public interface IPlayerCameraRotationControllerSettings
    {
        public float RotationAngle { get; }
        public float RotateDuration { get; }
        public Ease RotationAnimationEase { get; }
    }
}