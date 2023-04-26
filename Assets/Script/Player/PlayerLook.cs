using UnityEngine;

namespace Player
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _cameraRootTransform;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private float _upperLimit = -40f;
        [SerializeField] private float _bottomLimit = 70f;
        [SerializeField] private float _mouseSensitivity = 21f;
        private float _xRotation = 0f;
        private Transform _cameraTransform;
        private Transform _playerTransform;

        public Quaternion CameraRotation => _cameraTransform.rotation;
        public Vector3 CameraForward => _cameraTransform.forward;

        private void Awake()
        {
            _playerTransform = _playerController.transform;
            _cameraTransform = _camera.transform;
        }

        public void LookWithMouse(Vector2 mouseLookDelta)
        {
            _cameraTransform.position = _cameraRootTransform.position;
            _xRotation -= mouseLookDelta.y * _mouseSensitivity * Time.unscaledDeltaTime;
            _xRotation = Mathf.Clamp(_xRotation, _upperLimit, _bottomLimit);

            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            _playerTransform.Rotate(Vector3.up, mouseLookDelta.x * _mouseSensitivity * Time.unscaledDeltaTime);
        }
    }
}