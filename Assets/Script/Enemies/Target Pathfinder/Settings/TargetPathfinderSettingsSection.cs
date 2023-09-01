using System;
using UnityEngine;

namespace Enemies.Target_Pathfinder.Settings
{
    [Serializable]
    public class TargetPathfinderSettingsSection : ITargetPathfinderSettings
    {
        [SerializeField] private float _updateDestinationCooldownSeconds = 0.75f;
        [SerializeField] private float _nextWaypointDistance = 0.5f;
        [SerializeField] private float _maxDistanceFromTargetToNavMesh = 7f;

        public float MaxDistanceFromTargetToNavMesh => _maxDistanceFromTargetToNavMesh;
        public float UpdateDestinationCooldownSeconds => _updateDestinationCooldownSeconds;
        public float NextWaypointDistance => _nextWaypointDistance;
    }
}