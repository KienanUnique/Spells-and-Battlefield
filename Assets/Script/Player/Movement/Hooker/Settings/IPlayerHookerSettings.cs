using UnityEngine;

namespace Player.Movement.Hooker.Settings
{
    public interface IPlayerHookerSettings
    {
        public LayerMask Mask { get; }
        public float MaxDistance { get; }
        public float CancelHookDistance { get; }
        public float PointSelectionRadius { get; }
        public float MaxDurationInSeconds { get; }
        public float StartAndCurrentDirectionsMaxAngle { get; }
    }
}