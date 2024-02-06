using System;
using UnityEngine;

namespace Player.Movement.Hooker.Settings
{
    [Serializable]
    public class PlayerHookerSettings : IPlayerHookerSettings
    {
        [SerializeField] private LayerMask _mask;
        [SerializeField] [Min(0)] private float _maxDistance = 50f;
        [SerializeField] [Min(0)] private float _cancelHookDistance = 5f;
        [SerializeField] [Min(0)] private float _pointSelectionRadius = 5f;
        [SerializeField] [Min(0)] private float _startAndCurrentDirectionsMaxAngle = 90f;
        [SerializeField] private float _maxDurationInSeconds = 6f;

        public LayerMask Mask => _mask;
        public float MaxDistance => _maxDistance;
        public float CancelHookDistance => _cancelHookDistance;
        public float PointSelectionRadius => _pointSelectionRadius;
        public float MaxDurationInSeconds => _maxDurationInSeconds;
        public float StartAndCurrentDirectionsMaxAngle => _startAndCurrentDirectionsMaxAngle;
    }
}