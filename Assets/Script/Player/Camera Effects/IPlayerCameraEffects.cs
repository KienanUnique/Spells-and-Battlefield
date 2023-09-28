using Player.Movement;

namespace Player.Camera_Effects
{
    public interface IPlayerCameraEffects
    {
        public void Rotate(WallDirection direction);
        public void ResetRotation();
        public void PlayIncreaseFieldOfViewAnimation();
    }
}