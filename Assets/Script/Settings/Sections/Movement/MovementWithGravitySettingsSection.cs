using System;
using UnityEngine;

namespace Settings.Sections.Movement
{
    [Serializable]
    public class MovementOnGroundSettingsSection : MovementSettingsSection
    {
        [SerializeField] private float _normalGravityForce = 30;
        
        public float NormalGravityForce => _normalGravityForce;
    }
}