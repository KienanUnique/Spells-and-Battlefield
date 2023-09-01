using System;
using UnityEngine;

namespace Enemies.Target_Pathfinder.Settings
{
    [Serializable]
    public class TargetPathfinderSettingsSection : ITargetPathfinderSettings
    {
        [SerializeField] private float _updateDestinationCooldownSeconds = 0.75f;
        [SerializeField] private float _nextWaypointDistance = 0.5f;
        public float UpdateDestinationCooldownSeconds => _updateDestinationCooldownSeconds;
        public float NextWaypointDistance => _nextWaypointDistance;
    }
}