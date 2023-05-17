using Settings;
using UnityEngine;

namespace Player
{
    public class PlayerLook
    {
        private readonly Transform _cameraRootTransform;
        private readonly Transform _rotateObject;
        private readonly PlayerSettings.PlayerLookSettingsSection _lookSettings;
        private readonly Transform _cameraTransform;

        private float _xRotation;


        public PlayerLook(Camera camera, Transform cameraRootTransform, Transform rotateObject,
            PlayerSettings.PlayerLookSettingsSection lookSettings)
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
            _cameraTransform.position = _cameraRootTransform.position;
            _xRotation -= mouseLookDelta.y;
            _xRotation = Mathf.Clamp(_xRotation, _lookSettings.UpperLimit, _lookSettings.BottomLimit);

            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            _rotateObject.Rotate(Vector3.up, mouseLookDelta.x);
        }
    }
}