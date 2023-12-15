using UnityEngine;

namespace Common.Look
{
    public interface ILookSettings
    {
        public float MaxAimRaycastDistance { get; }
        public LayerMask AimLayerMask { get; }
    }
}