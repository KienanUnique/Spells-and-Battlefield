using System;
using UnityEngine;

namespace Player.Look.Settings
{
    [Serializable]
    public class PlayerLookSettingsSection : IPlayerLookSettings
    {
        [SerializeField] private float _upperLimit = -40f;
        [SerializeField] private float _bottomLimit = 70f;
        [SerializeField] private LayerMask _aimLayerMask;

        public float UpperLimit => _upperLimit;
        public float BottomLimit => _bottomLimit;
        public LayerMask AimLayerMask => _aimLayerMask;
    }
}