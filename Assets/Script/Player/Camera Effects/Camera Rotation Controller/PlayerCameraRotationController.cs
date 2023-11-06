using System;
using DG.Tweening;
using Player.Movement;
using UnityEngine;

namespace Player.Camera_Effects.Camera_Rotator
{
    public class PlayerCameraRotationController : IPlayerCameraRotationController
    {
        private readonly Transform _cameraTransform;
        private readonly IPlayerCameraRotationControllerSettings _settings;
        private readonly Vector3 _defaultRotation;
        private readonly GameObject _gameObjectToLink;
        private Sequence _rotateAnimationSequence;

        public PlayerCameraRotationController(Transform cameraTransform,
            IPlayerCameraRotationControllerSettings settings)
        {
            _cameraTransform = cameraTransform;
            _settings = settings;
            _gameObjectToLink = _cameraTransform.gameObject;
            _defaultRotation = _cameraTransform.localRotation.eulerAngles;
        }

        public void Rotate(WallDirection direction)
        {
            var needRotation = new Vector3(0, 0, direction switch
            {
                WallDirection.Left => _settings.RotationAngle * -1,
                WallDirection.Right => _settings.RotationAngle,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            });
            Rotate(needRotation);
        }

        public void ResetRotation()
        {
            Rotate(_defaultRotation);
        }

        private void Rotate(Vector3 targetRotation)
        {
            _rotateAnimationSequence?.Kill();
            _rotateAnimationSequence = DOTween.Sequence();
            _rotateAnimationSequence
                .Append(_cameraTransform.DOLocalRotate(targetRotation, _settings.RotateDuration)
                                        .SetEase(_settings.RotationAnimationEase))
                .SetLink(_gameObjectToLink)
                .OnComplete(() => _rotateAnimationSequence = null);
        }
    }
}