using Common.Readonly_Transform;
using UnityEngine;

namespace Enemies.Target_Pathfinder
{
    public interface ITargetPathfinderForMovement
    {
        public Vector3 CurrentWaypoint { get; }

        public void StartUpdatingPathForKeepingTransformOnDistance(IReadonlyTransform targetPosition,
            float needDistance);

        public void StopUpdatingPath();
        public bool IsPathComplete();
    }
}