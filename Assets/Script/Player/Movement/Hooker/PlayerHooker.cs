using System;
using System.Collections;
using Common.Interfaces;
using Common.Readonly_Transform;
using Player.Look;
using Player.Movement.Hooker.Settings;
using UnityEngine;

namespace Player.Movement.Hooker
{
    public class PlayerHooker : IPlayerHooker
    {
        private readonly IReadonlyTransform _rigidbody;
        private readonly IReadonlyPlayerLook _look;
        private readonly IPlayerHookerSettings _hookSettings;
        private readonly ICoroutineStarter _coroutineStarter;
        private Vector3 _hookPoint;

        public PlayerHooker(IReadonlyTransform rigidbody, IReadonlyPlayerLook look, IPlayerHookerSettings hookSettings,
            ICoroutineStarter coroutineStarter)
        {
            _rigidbody = rigidbody;
            _look = look;
            _hookSettings = hookSettings;
            _coroutineStarter = coroutineStarter;
        }

        public event Action HookingEnded;

        public Vector3 HookPushDirection { get; private set; }
        public Vector3 HookLookPoint { get; private set; }
        public bool IsHooking { get; private set; }

        public bool TrySetHookPoint()
        {
            if (!_look.TryCalculateLookPointWithSphereCast(out Vector3 lookPoint, _hookSettings.MaxDistance,
                    _hookSettings.PointSelectionRadius, _hookSettings.Mask))
            {
                return false;
            }

            var originalHookPointY = lookPoint.y;
            
            // TODO: replace sphere cast to raycast. If in sphere near point there is IHookPointProvider => get point from it 
            lookPoint.y = originalHookPointY + _hookSettings.PushPointYOffset;
            _hookPoint = lookPoint;
            
            lookPoint.y = originalHookPointY + _hookSettings.LookPointYOffset;
            HookLookPoint = lookPoint;
            
            return true;
        }

        public void StartCalculatingHookDirection()
        {
            IsHooking = true;
            _coroutineStarter.StartCoroutine(RemoveHookAfterTimeOut());
            _coroutineStarter.StartCoroutine(CalculateHookDirection());
        }

        private IEnumerator CalculateHookDirection()
        {
            Vector3 distance;
            while (IsHooking)
            {
                distance = _hookPoint - _rigidbody.Position;
                if (distance.magnitude < _hookSettings.MinHookDistance)
                {
                    Debug.Log("Hook ended (CalculateHookDirection)");
                    IsHooking = false;
                    HookingEnded?.Invoke();
                }

                HookPushDirection = distance.normalized;
                yield return null;
            }
        }

        private IEnumerator RemoveHookAfterTimeOut()
        {
            yield return new WaitForSeconds(_hookSettings.Duration);
            if (IsHooking)
            {
                Debug.Log("Hook ended (RemoveHookAfterTimeOut)");
                IsHooking = false;
                HookingEnded?.Invoke();
            }
        }
    }
}