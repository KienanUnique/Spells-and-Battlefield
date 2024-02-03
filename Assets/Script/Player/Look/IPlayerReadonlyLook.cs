using Common;
using UnityEngine;

namespace Player.Look
{
    public interface IReadonlyPlayerLook : IReadonlyLook
    {
        public bool TryCalculateLookPointWithMaxDistance(out Vector3 lookPoint, float maxDistance);
    }
}