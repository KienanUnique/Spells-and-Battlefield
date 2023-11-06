using Player.Camera_Effects.Camera_Field_Of_View_Calculator;
using Player.Camera_Effects.Camera_Particle_System_Controller;
using Player.Camera_Effects.Camera_Rotator;
using Player.Movement;

namespace Player.Camera_Effects
{
    public class PlayerCameraEffects : IPlayerCameraEffects
    {
        private readonly IPlayerCameraRotationController _rotationController;
        private readonly IPlayerCameraFieldOfViewController _fieldOfViewController;
        private readonly IPlayerCameraParticleSystemController _particleSystemController;

        public PlayerCameraEffects(IPlayerCameraRotationController rotationController,
            IPlayerCameraFieldOfViewController fieldOfViewController,
            IPlayerCameraParticleSystemController particleSystemController)
        {
            _rotationController = rotationController;
            _fieldOfViewController = fieldOfViewController;
            _particleSystemController = particleSystemController;
        }

        public void UpdateOverSpeedValue(float newOverSpeedValue)
        {
            _fieldOfViewController.UpdateOverSpeedValue(newOverSpeedValue);
            _particleSystemController.UpdateOverSpeedValue(newOverSpeedValue);
        }

        public void PlayIncreaseFieldOfViewAnimation()
        {
            _fieldOfViewController.PlayIncreaseFieldOfViewAnimation();
        }

        public void Rotate(WallDirection direction)
        {
            _rotationController.Rotate(direction);
        }

        public void ResetRotation()
        {
            _rotationController.ResetRotation();
        }
    }
}