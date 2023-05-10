using System;
using DG.Tweening;
using UnityEngine;

namespace General_Settings_in_Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Player Settings",
        menuName = "Spells and Battlefield/Settings/General Settings/Player Settings", order = 0)]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private PlayerCameraEffectsSettingsSection _cameraEffects;
        [SerializeField] private PlayerLookSettingsSection _look;
        [SerializeField] private PlayerMovementSettingsSection _movement;
        [SerializeField] private CharacterSettingsSection _character;

        public PlayerCameraEffectsSettingsSection CameraEffects => _cameraEffects;
        public PlayerLookSettingsSection Look => _look;
        public PlayerMovementSettingsSection Movement => _movement;
        public CharacterSettingsSection Character => _character;

        [Serializable]
        public class PlayerCameraEffectsSettingsSection
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

        [Serializable]
        public class PlayerLookSettingsSection
        {
            [SerializeField] private float _upperLimit = -40f;
            [SerializeField] private float _bottomLimit = 70f;

            public float UpperLimit => _upperLimit;
            public float BottomLimit => _bottomLimit;
        }

        [Serializable]
        public class PlayerMovementSettingsSection : MovementSettingsSectionBase
        {
            [SerializeField] private float _jumpForce = 800f;
            [SerializeField] private float _dashForce = 1500f;
            [SerializeField] private float _normalGravityForce = 30;
            [SerializeField] private float _wallRunningGravityForce = 2;
            [SerializeField] private float _dashCooldownSeconds = 5f;
            [SerializeField] private float _dashSpeedLimitationsDisablingForSeconds = 0.3f;

            public float JumpForce => _jumpForce;
            public float DashForce => _dashForce;
            public float NormalGravityForce => _normalGravityForce;
            public float WallRunningGravityForce => _wallRunningGravityForce;
            public float DashCooldownSeconds => _dashCooldownSeconds;
            public float DashSpeedLimitationsDisablingForSeconds => _dashSpeedLimitationsDisablingForSeconds;
        }
    }
}