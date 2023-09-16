using Player.Movement;

namespace Player.Camera_Effects
{
    public interface IPlayerCameraEffects
    {
        void Rotate(WallDirection direction);
        void ResetRotation();
        void PlayIncreaseFieldOfViewAnimation();
    }
}