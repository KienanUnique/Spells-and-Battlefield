using Common;
using UnityEngine;

namespace Player.Look
{
    public interface IReadonlyPlayerLook : IReadonlyLook
    {
        public Vector3 CalculateLookPointWithSphereCast(float maxDistance, float sphereRadius, LayerMask layerMask);
    }
}