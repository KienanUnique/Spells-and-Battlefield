using Player.Camera_Effects.Camera_Field_Of_View_Calculator;
using Player.Camera_Effects.Camera_Particle_System_Controller;
using Player.Camera_Effects.Camera_Rotator;

namespace Player.Camera_Effects.Settings
{
    public interface IPlayerCameraEffectsSettings : IPlayerCameraRotationControllerSettings,
        IPlayerCameraFOVSettings,
        IPlayerCameraParticleSystemSettings
    {
    }
}