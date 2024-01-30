using System;
using UnityEngine;

namespace Common.Settings.Sections.Movement
{
    [Serializable]
    public class MovementSettingsSection : IMovementSettingsSection
    {
        [SerializeField] private float _moveForce = 4500f;
        [SerializeField] private float _maximumSpeed = 15f;
        [SerializeField] private float _hookForce = 5000f;
        [Range(0, 1f)] [SerializeField] private float _normalFrictionCoefficient = 0.175f;

        public float NormalFrictionCoefficient => _normalFrictionCoefficient;
        public float MoveForce => _moveForce;
        public float MaximumSpeed => _maximumSpeed;
        public float HookForce => _hookForce;
    }
}