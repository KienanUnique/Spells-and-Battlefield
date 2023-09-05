using System.Collections.Generic;
using Puzzles.Mechanisms.Moving_Platforms.Settings;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating
{
    public interface IMovingPlatformDataForControllerBase
    {
        float DelayInSeconds { get; }
        IMovingPlatformsSettings Settings { get; }
        float MovementSpeed { get; }
        List<Vector3> Waypoints { get; }
        Transform ObjectToMove { get; }
        IColliderTrigger PlatformCollider { get; }
    }
}