using System;
using DG.Tweening;
using Settings;
using UnityEngine;

namespace Player
{
    public class PlayerCameraEffects
    {
        private const RotateMode CameraRotateMode = RotateMode.Fast;

        private readonly Camera _camera;
        private readonly PlayerSettings.PlayerCameraEffectsSettingsSection _cameraEffectsSettings;
        private readonly GameObject _effectsGameObject;

        private readonly Vector3 _defaultRotation;
        private readonly Transform _cachedTransform;


        public PlayerCameraEffects(PlayerSettings.PlayerCameraEffectsSettingsSection cameraEffectsSettings,
            Camera camera, GameObject effectsGameObject)
        {
            _camera = camera;
            _cameraEffectsSettings = cameraEffectsSettings;
            _effectsGameObject = effectsGameObject;

            _cachedTransform = _effectsGameObject.transform;
            _defaultRotation = _cachedTransform.localRotation.eulerAngles;
            _camera.fieldOfView = _cameraEffectsSettings.CameraNormalFOV;
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
                .SetLink(_effectsGameObject);
        }

        public void ResetRotation()
        {
            _cachedTransform.DOKill();
            _cachedTransform.DOLocalRotate(_defaultRotation, _cameraEffectsSettings.RotateDuration, CameraRotateMode)
                .SetLink(_effectsGameObject);
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
    }
}