using System;
using UnityEngine;

namespace Player.Movement.Hooker
{
    public interface IPlayerHooker
    {
        public event Action HookingEnded;
        public Vector3 HookPushDirection { get; }
        public Vector3 HookPoint { get; }
        public bool IsHooking { get; }        
        public bool TrySetHookPoint();
        public void StartCalculatingHookDirection();
    }
}