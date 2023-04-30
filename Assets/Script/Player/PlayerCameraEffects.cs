using System;
using DG.Tweening;
using UnityEngine;

namespace Player
{
    public class PlayerCameraEffects : MonoBehaviour
    {
        private const RotateMode CameraRotateMode = RotateMode.Fast;
        [SerializeField] private float _rotationAngle = 30f;
        [SerializeField] private float _rotateDuration;
        [SerializeField] private float _cameraIncreasedFOV = 130f;
        [SerializeField] private float _cameraNormalFOV = 100f;
        [SerializeField] private float _changeCameraFOVAnimationDuration = 0.3f;
        [SerializeField] private Ease _changeCameraFOVAnimationEase = Ease.OutCubic;
        [SerializeField] private Camera _camera;
        private Vector3 _defaultRotation;
        private Transform _cachedTransform;

        public void Rotate(WallDirection direction)
        {
            _cachedTransform.DOKill();
            var needRotation = new Vector3(0, 0, direction switch
            {
                WallDirection.Left => _rotationAngle * -1,
                WallDirection.Right => _rotationAngle,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            });
            _cachedTransform.DOLocalRotate(needRotation, _rotateDuration, CameraRotateMode).SetLink(gameObject);
        }

        public void ResetRotation()
        {
            _cachedTransform.DOKill();
            _cachedTransform.DOLocalRotate(_defaultRotation, _rotateDuration, CameraRotateMode).SetLink(gameObject);
        }

        public void PlayIncreaseFieldOfViewAnimation()
        {
            _camera.DOKill();
            _camera.DOFieldOfView(_cameraIncreasedFOV, _changeCameraFOVAnimationDuration)
                .SetEase(_changeCameraFOVAnimationEase).OnComplete(() => _camera
                    .DOFieldOfView(_cameraNormalFOV, _changeCameraFOVAnimationDuration)
                    .SetEase(_changeCameraFOVAnimationEase));
        }

        private void Awake()
        {
            _cachedTransform = transform;
            _defaultRotation = _cachedTransform.localRotation.eulerAngles;
        }

        private void Start()
        {
            _camera.fieldOfView = _cameraNormalFOV;
        }
    }
}