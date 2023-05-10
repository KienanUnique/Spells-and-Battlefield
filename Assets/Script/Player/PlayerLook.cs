using Game_Managers;
using General_Settings_in_Scriptable_Objects;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _cameraRootTransform;
        [SerializeField] private Transform _rotateObject;
        private float _xRotation = 0f;
        private Transform _cameraTransform;
        private PlayerSettings.PlayerLookSettingsSection _lookSettings;
        
        [Inject]
        private void Construct(PlayerSettings settings)
        {
            _lookSettings = settings.Look;
        }

        public Quaternion CameraRotation => _cameraTransform.rotation;
        public Vector3 CameraForward => _cameraTransform.forward;

        private void Awake()
        {
            _cameraTransform = _camera.transform;
        }

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