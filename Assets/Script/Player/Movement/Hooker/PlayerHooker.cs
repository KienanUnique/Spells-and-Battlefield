using System;
using Common;
using Common.Interfaces;
using Common.Readonly_Transform;
using Player.Look;
using Player.Movement.Hooker.Settings;
using UnityEngine;

namespace Player.Movement.Hooker
{
    public class PlayerHooker : IPlayerHooker
    {
        private readonly IReadonlyTransform _pullPoint;
        private readonly IReadonlyPlayerLook _look;
        private readonly IPlayerHookerSettings _hookSettings;
        private readonly ICoroutineStarter _coroutineStarter;
        private readonly Vector3 _hookPushDirection;
        private readonly bool _isHooking;
        private Vector3 _hookedPoint;
        private Coroutine _calculationCoroutine;

        public event Action HookingEnded;

        public Vector3 HookPushDirection => _hookPushDirection;
        public bool IsHooking => _isHooking;

        public bool TrySetHookPoint()
        {
            _look.CalculateLookPointWithSphereCast(_hookSettings.MaxDistance, _hookSettings.PointSelectionRadius,
                _hookSettings.Mask);
        }

        public void StartCalculatingHookDirection()
        {
        }
    }
}