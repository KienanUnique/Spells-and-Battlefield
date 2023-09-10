using UnityEngine;

namespace Player.Look.Settings
{
    public interface IPlayerLookSettings
    {
        public float UpperLimit { get; }
        public float BottomLimit { get; }
        public LayerMask AimLayerMask { get; }
    }
}