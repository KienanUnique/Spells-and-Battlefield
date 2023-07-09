using System.Collections.Generic;
using Puzzles.Triggers.Box_Collider_Trigger;
using Settings.Puzzles.Mechanisms;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating
{
    public interface IMovingPlatformDataForControllerBase
    {
        float DelayInSeconds { get; }
        MovingPlatformsSettings Settings { get; }
        float MovementSpeed { get; }
        List<Vector3> Waypoints { get; }
        Transform ObjectToMove { get; }
        IColliderTrigger PlatformCollider { get; }
    }
}