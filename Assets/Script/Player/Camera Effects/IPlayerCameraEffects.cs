using Player.Camera_Effects.Camera_Field_Of_View_Calculator;
using Player.Camera_Effects.Camera_Rotator;

namespace Player.Camera_Effects
{
    public interface IPlayerCameraEffects : IPlayerCameraFieldOfViewController, IPlayerCameraRotationController
    {
    }
}