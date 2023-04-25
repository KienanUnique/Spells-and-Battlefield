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

        private void Awake()
        {
            _cachedTransform = transform;
            _defaultRotation = _cachedTransform.localRotation.eulerAngles;
        }
    }
}