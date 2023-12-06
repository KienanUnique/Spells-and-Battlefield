using System;
using UnityEngine;

namespace Common.Look
{
    [Serializable]
    public class LookSettingsSection : ILookSettings
    {
        [SerializeField] private float _maxAimRaycastDistance = 400f;
        [SerializeField] private LayerMask _aimLayerMask;

        public float MaxAimRaycastDistance => _maxAimRaycastDistance;
        public LayerMask AimLayerMask => _aimLayerMask;
    }
}