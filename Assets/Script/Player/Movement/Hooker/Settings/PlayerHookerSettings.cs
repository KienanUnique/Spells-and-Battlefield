using System;
using UnityEngine;

namespace Player.Movement.Hooker.Settings
{
    [Serializable]
    public class PlayerHookerSettings : IPlayerHookerSettings
    {
        [SerializeField] private LayerMask _mask;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _minHookDistance;
        [SerializeField] private float _pointSelectionRadius;
        [SerializeField] private float _duration;
        [SerializeField] private float _pointYOffset;

        public LayerMask Mask => _mask;
        public float MaxDistance => _maxDistance;
        public float MinHookDistance => _minHookDistance;
        public float PointSelectionRadius => _pointSelectionRadius;
        public float PointYOffset => _pointYOffset;
        public float Duration => _duration;
    }
}