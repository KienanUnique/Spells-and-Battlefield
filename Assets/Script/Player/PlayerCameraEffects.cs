using System;
using DG.Tweening;
using General_Settings_in_Scriptable_Objects;
using Settings;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerCameraEffects : MonoBehaviour
    {
        private const RotateMode CameraRotateMode = RotateMode.Fast;
        [SerializeField] private Camera _camera;
        private Vector3 _defaultRotation;
        private Transform _cachedTransform;
        private PlayerSettings.PlayerCameraEffectsSettingsSection _cameraEffectsSettings;

        [Inject]
        private void Construct(PlayerSettings settings)
        {
            _cameraEffectsSettings = settings.CameraEffects;
        }

        public void Rotate(WallDirection direction)
        {
            _cachedTransform.DOKill();
            var needRotation = new Vector3(0, 0, direction switch
            {
                WallDirection.Left => _cameraEffectsSettings.RotationAngle * -1,
                WallDirection.Right => _cameraEffectsSettings.RotationAngle,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            });
            _cachedTransform.DOLocalRotate(needRotation, _cameraEffectsSettings.RotateDuration, CameraRotateMode)
                .SetLink(gameObject);
        }

        public void ResetRotation()
        {
            _cachedTransform.DOKill();
            _cachedTransform.DOLocalRotate(_defaultRotation, _cameraEffectsSettings.RotateDuration, CameraRotateMode)
                .SetLink(gameObject);
        }

        public void PlayIncreaseFieldOfViewAnimation()
        {
            _camera.DOKill();
            _camera.DOFieldOfView(_cameraEffectsSettings.CameraIncreasedFOV,
                    _cameraEffectsSettings.ChangeCameraFOVAnimationDuration)
                .SetEase(_cameraEffectsSettings.ChangeCameraFOVAnimationEase).OnComplete(() => _camera
                    .DOFieldOfView(_cameraEffectsSettings.CameraNormalFOV,
                        _cameraEffectsSettings.ChangeCameraFOVAnimationDuration)
                    .SetEase(_cameraEffectsSettings.ChangeCameraFOVAnimationEase));
        }

        private void Awake()
        {
            _cachedTransform = transform;
            _defaultRotation = _cachedTransform.localRotation.eulerAngles;
        }

        private void Start()
        {
            _camera.fieldOfView = _cameraEffectsSettings.CameraNormalFOV;
        }
    }
}