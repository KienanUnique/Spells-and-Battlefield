using Player.Movement;

namespace Player.Camera_Effects.Camera_Rotator
{
    public interface IPlayerCameraRotationController
    {
        public void Rotate(WallDirection direction);
        public void ResetRotation();
    }
}