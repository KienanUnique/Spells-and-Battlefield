using UnityEngine;

namespace Player.Look
{
    public interface IPlayerLook
    {
        public Quaternion CameraRotation { get; }
        public Vector3 CameraForward { get; }
        public void LookInputtedWith(Vector2 mouseLookDelta);
    }
}