using System;
using UnityEngine;

namespace General_Settings_in_Scriptable_Objects
{
    [Serializable]
    public class TargetPathfinderSettingsSection
    {
        [SerializeField] private float _updateDestinationCooldownSeconds = 0.75f;
        [SerializeField] private float _nextWaypointDistance = 2f;
        public float UpdateDestinationCooldownSeconds => _updateDestinationCooldownSeconds;
        public float NextWaypointDistance => _nextWaypointDistance;
    }
}