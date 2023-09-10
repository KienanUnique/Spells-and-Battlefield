using Common.Readonly_Transform;
using Player.Look.Settings;
using UnityEngine;

namespace Player.Look
{
    public class PlayerLook : IPlayerLook
    {
        private const float MaxAimRaycastDistance = 400f;
        private readonly IReadonlyTransform _cameraRootTransform;
        private readonly Transform _cameraTransform;
        private readonly IPlayerLookSettings _lookSettings;
        private readonly Transform _rotateObject;
        private RaycastHit _cameraForwardRaycastHit;

        private float _xRotation;

        public PlayerLook(Camera camera, IReadonlyTransform cameraRootTransform, Transform rotateObject,
            IPlayerLookSettings lookSettings)
        {
            _cameraRootTransform = cameraRootTransform;
            _rotateObject = rotateObject;
            _lookSettings = lookSettings;
            _cameraTransform = camera.transform;
        }

        public Vector3 CameraLookPointPosition
        {
            get
            {
                if (Physics.Raycast(_cameraTransform.position, _cameraTransform.forward, out _cameraForwardRaycastHit,
                        MaxAimRaycastDistance, _lookSettings.AimLayerMask, QueryTriggerInteraction.Ignore))
                {
                    return _cameraForwardRaycastHit.point;
                }

                return _cameraTransform.position + _cameraTransform.forward * MaxAimRaycastDistance;
            }
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