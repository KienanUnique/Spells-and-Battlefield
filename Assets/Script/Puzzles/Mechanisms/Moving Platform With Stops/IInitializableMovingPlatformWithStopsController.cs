using System.Collections.Generic;
using Puzzles.Triggers;
using Puzzles.Triggers.Box_Collider_Trigger;
using Settings.Puzzles.Mechanisms;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platform_With_Stops
{
    public interface IInitializableMovingPlatformWithStopsController
    {
        public void Initialize(Transform objectToMove, List<ITrigger> moveNextTriggers,
            List<ITrigger> movePreviousTriggers, List<Vector3> waypoints, float movementSpeed,
            MovingPlatformWithStopsSettings settings, IColliderTrigger platformCollider);
    }
}