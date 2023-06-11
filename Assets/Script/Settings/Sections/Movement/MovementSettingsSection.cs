using System;
using UnityEngine;

namespace Settings.Sections.Movement
{
    [Serializable]
    public class MovementSettingsSection
    {
        [SerializeField] private float _moveForce = 4500f;
        [SerializeField] private float _maximumSpeed = 15f;
        [Range(0, 1f)] [SerializeField] private float _normalFrictionCoefficient = 0.175f;

        public float NormalFrictionCoefficient => _normalFrictionCoefficient;
        public float MoveForce => _moveForce;
        public float MaximumSpeed => _maximumSpeed;
    }
}