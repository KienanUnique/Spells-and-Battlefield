using System;
using DG.Tweening;
using UnityEngine;

namespace Player.Camera_Effects.Settings
{
    [Serializable]
    public class PlayerCameraEffectsSettingsSection : IPlayerCameraEffectsSettings
    {
        [SerializeField] private float _rotationAngle = 30f;
        [SerializeField] private float _rotateDuration = 0.5f;
        [SerializeField] private float _cameraIncreasedFOV = 130f;
        [SerializeField] private float _cameraNormalFOV = 100f;
        [SerializeField] private float _changeCameraFOVAnimationDuration = 0.1f;
        [SerializeField] private Ease _changeCameraFOVAnimationEase = Ease.OutCubic;

        public float RotationAngle => _rotationAngle;
        public float RotateDuration => _rotateDuration;
        public float CameraIncreasedFOV => _cameraIncreasedFOV;
        public float CameraNormalFOV => _cameraNormalFOV;
        public float ChangeCameraFOVAnimationDuration => _changeCameraFOVAnimationDuration;
        public Ease ChangeCameraFOVAnimationEase => _changeCameraFOVAnimationEase;
    }
}