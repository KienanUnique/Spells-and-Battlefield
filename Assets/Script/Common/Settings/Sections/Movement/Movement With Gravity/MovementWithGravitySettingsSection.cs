using System;
using UnityEngine;

namespace Common.Settings.Sections.Movement.Movement_With_Gravity
{
    [Serializable]
    public class MovementOnGroundSettingsSection : MovementSettingsSection, IMovementOnGroundSettingsSection
    {
        [SerializeField] private float _normalGravityForce = 30;
        
        public float NormalGravityForce => _normalGravityForce;
    }
}