using UnityEngine;

namespace Player.Look
{
    public interface IPlayerLook
    {
        Quaternion CameraRotation { get; }
        Vector3 CameraForward { get; }
        void LookInputtedWith(Vector2 mouseLookDelta);
    }
}