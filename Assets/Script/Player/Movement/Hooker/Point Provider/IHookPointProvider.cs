using UnityEngine;

namespace Player.Movement.Hooker.Point_Provider
{
    public interface IHookPointProvider
    {
        public Vector3 HookPoint { get; }
    }
}