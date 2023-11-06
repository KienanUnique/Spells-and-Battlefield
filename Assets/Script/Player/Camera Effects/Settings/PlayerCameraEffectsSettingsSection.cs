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
        [SerializeField] private Ease _changeCameraFOVAnimationEase = Ease.OutCubic;
        [SerializeField] private Ease _rotationAnimationEase;
        [SerializeField] private float _baseFOV = 100f;
        [SerializeField] private float _dashFOV = 140f;
        [SerializeField] private float _maximumOverSpeedFieldOfViewAdditionalValue = 40f;
        [SerializeField] private float _onDashFOVChangeAnimationDuration = 0.1f;
        [SerializeField] private float _overSpeedFOVChangeSpeed = 100;
        [SerializeField] private float _maximumEffectsOverSpeedValue = 5f;
        public float BaseFOV => _baseFOV;
        public float DashFOV => _dashFOV;
        public float MaximumOverSpeedFieldOfViewAdditionalValue => _maximumOverSpeedFieldOfViewAdditionalValue;
        public float OnDashFOVChangeAnimationDuration => _onDashFOVChangeAnimationDuration;
        public float OverSpeedFOVChangeSpeed => _overSpeedFOVChangeSpeed;
        public Ease ChangeCameraFOVAnimationEase => _changeCameraFOVAnimationEase;

        public float MaximumEffectsOverSpeedValue => _maximumEffectsOverSpeedValue;

        public float RotationAngle => _rotationAngle;
        public float RotateDuration => _rotateDuration;
        public Ease RotationAnimationEase => _rotationAnimationEase;
    }
}