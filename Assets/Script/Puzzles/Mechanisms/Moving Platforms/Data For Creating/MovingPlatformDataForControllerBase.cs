using System.Collections.Generic;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Settings.Puzzles.Mechanisms;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating
{
    public class MovingPlatformDataForControllerBase : IMovingPlatformDataForControllerBase
    {
        public MovingPlatformDataForControllerBase(float delayInSeconds, MovingPlatformsSettings settings,
            float movementSpeed, List<Vector3> waypoints, Transform objectToMove,
            IColliderTrigger platformCollider)
        {
            DelayInSeconds = delayInSeconds;
            Settings = settings;
            MovementSpeed = movementSpeed;
            Waypoints = waypoints;
            ObjectToMove = objectToMove;
            PlatformCollider = platformCollider;
        }

        public float DelayInSeconds { get; }
        public MovingPlatformsSettings Settings { get; }
        public float MovementSpeed { get; }
        public List<Vector3> Waypoints { get; }
        public Transform ObjectToMove { get; }
        public IColliderTrigger PlatformCollider { get; }
    }
}