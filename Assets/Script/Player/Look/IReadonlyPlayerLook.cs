using UnityEngine;

namespace Player.Look
{
    public interface IReadonlyPlayerLook
    {
        public Vector3 CameraLookPointPosition { get; }
        public Quaternion CameraRotation { get; }
        public Vector3 CameraForward { get; }
    }
}