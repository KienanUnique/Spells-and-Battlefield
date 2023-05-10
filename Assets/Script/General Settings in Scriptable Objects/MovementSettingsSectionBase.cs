using System;
using UnityEngine;

namespace General_Settings_in_Scriptable_Objects
{
    [Serializable]
    public class MovementSettingsSectionBase
    {
        [SerializeField] protected float _moveForce = 4500f;
        [SerializeField] protected float _maximumSpeed = 15f;
        [Range(0, 1f)] [SerializeField] protected float _frictionCoefficient = 0.175f;

        public float MoveForce => _moveForce;
        public float MaximumSpeed => _maximumSpeed;
        public float FrictionCoefficient => _frictionCoefficient;
    }
}