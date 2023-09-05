using Common.Readonly_Transform;
using Player.Look.Settings;
using Player.Settings;
using UnityEngine;

namespace Player.Look
{
    public class PlayerLook : IPlayerLook
    {
        private readonly IReadonlyTransform _cameraRootTransform;
        private readonly Transform _rotateObject;
        private readonly IPlayerLookSettings _lookSettings;
        private readonly Transform _cameraTransform;

        private float _xRotation;


        public PlayerLook(Camera camera, IReadonlyTransform cameraRootTransform, Transform rotateObject,
            IPlayerLookSettings lookSettings)
        {
            _cameraRootTransform = cameraRootTransform;
            _rotateObject = rotateObject;
            _lookSettings = lookSettings;
            _cameraTransform = camera.transform;
        }

        public Quaternion CameraRotation => _cameraTransform.rotation;
        public Vector3 CameraForward => _cameraTransform.forward;

        public void LookInputtedWith(Vector2 mouseLookDelta)
        {
            _cameraTransform.position = _cameraRootTransform.Position;
            _xRotation -= mouseLookDelta.y;
            _xRotation = Mathf.Clamp(_xRotation, _lookSettings.UpperLimit, _lookSettings.BottomLimit);

            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            _rotateObject.Rotate(Vector3.up, mouseLookDelta.x);
        }
    }
}