using UnityEngine;

namespace Interfaces
{
    public interface IToMovingPlatformStickable
    {
        public void StickToPlatform(Transform platformTransform);
        public void UnstickFromPlatform();
    }
}