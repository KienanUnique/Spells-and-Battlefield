using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms
{
    public interface IToMovingPlatformStickable
    {
        public void StickToPlatform(Transform platformTransform);
        public void UnstickFromPlatform();
    }
}