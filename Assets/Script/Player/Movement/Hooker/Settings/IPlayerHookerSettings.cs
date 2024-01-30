using UnityEngine;

namespace Player.Movement.Hooker.Settings
{
    public interface IPlayerHookerSettings
    {
        public LayerMask Mask { get; }
        public float MaxDistance { get; }
        public float MinHookDistance { get; }
        public float PointSelectionRadius { get; }
        public float Duration { get; }
    }
}