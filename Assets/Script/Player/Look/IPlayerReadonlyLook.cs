using Common;
using UnityEngine;

namespace Player.Look
{
    public interface IReadonlyPlayerLook : IReadonlyLook
    {
        public bool TryCalculateLookPointWithSphereCast(out Vector3 lookPoint, float maxDistance, float sphereRadius,
            LayerMask layerMask);
    }
}