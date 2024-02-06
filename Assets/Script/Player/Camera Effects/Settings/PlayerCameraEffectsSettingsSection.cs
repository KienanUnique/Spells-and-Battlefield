using System;
using DG.Tweening;
using UnityEngine;

namespace Player.Camera_Effects.Settings
{
    [Serializable]
    public class PlayerCameraEffectsSettingsSection : IPlayerCameraEffectsSettings
    {
        [Header("Rotation")] [SerializeField] private float _rotationAngle = 30f;
        [SerializeField] private float _rotateDuration = 0.5f;
        [SerializeField] private Ease _rotationAnimationEase = Ease.OutCubic;

        [Header("FOV")] [SerializeField] private Ease _changeCameraFOVAnimationEase = Ease.OutCubic;
        [SerializeField] private float _baseFOV = 100f;
        [SerializeField] private float _dashFOV = 140f;
        [SerializeField] private float _maximumOverSpeedFieldOfViewAdditionalValue = 15f;
        [SerializeField] private float _onDashFOVChangeAnimationDuration = 0.1f;
        [SerializeField] private float _overSpeedFOVChangeSpeed = 100;
        [SerializeField] private float _maximumOverSpeedValueForFieldOfView = 5f;

        [Header("Wind Trail Effects")]
        [SerializeField]
        private float _overSpeedMaximumEmissionRateOverTime = 200f;

        [SerializeField] private float _maximumOverSpeedValueForWindTrailEffect = 10f;
        [SerializeField] private float _minimumOverSpeedValueForWindTrailEffect = 5f;

        public float BaseFOV => _baseFOV;
        public float DashFOV => _dashFOV;
        public float MaximumOverSpeedFieldOfViewAdditionalValue => _maximumOverSpeedFieldOfViewAdditionalValue;
        public float OnDashFOVChangeAnimationDuration => _onDashFOVChangeAnimationDuration;
        public float OverSpeedFOVChangeSpeed => _overSpeedFOVChangeSpeed;
        public Ease ChangeCameraFOVAnimationEase => _changeCameraFOVAnimationEase;

        public float MaximumOverSpeedValueForFieldOfView => _maximumOverSpeedValueForFieldOfView;
        public float OverSpeedMaximumEmissionRateOverTime => _overSpeedMaximumEmissionRateOverTime;

        public float MaximumOverSpeedValueForWindTrailEffect => _maximumOverSpeedValueForWindTrailEffect;

        public float MinimumOverSpeedValueForWindTrailEffect => _minimumOverSpeedValueForWindTrailEffect;

        public float RotationAngle => _rotationAngle;
        public float RotateDuration => _rotateDuration;
        public Ease RotationAnimationEase => _rotationAnimationEase;
    }
}