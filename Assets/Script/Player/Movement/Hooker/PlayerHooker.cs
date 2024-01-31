using System;
using System.Collections;
using Common.Interfaces;
using Common.Readonly_Rigidbody;
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
        private Vector3 _hookPushDirection;
        private bool _isHooking;
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

        public Vector3 HookPushDirection => _hookPushDirection;
        public bool IsHooking => _isHooking;

        public bool TrySetHookPoint()
        {
            if (!_look.TryCalculateLookPointWithSphereCast(out Vector3 lookPoint, _hookSettings.MaxDistance,
                    _hookSettings.PointSelectionRadius, _hookSettings.Mask))
            {
                return false;
            }

            _hookPoint = lookPoint;
            return true;
        }

        public void StartCalculatingHookDirection()
        {
            _isHooking = true;
            _coroutineStarter.StartCoroutine(RemoveHookAfterTimeOut());
            _coroutineStarter.StartCoroutine(CalculateHookDirection());
        }

        private IEnumerator CalculateHookDirection()
        {
            Vector3 distance;
            while (_isHooking)
            {
                distance = _hookPoint - _rigidbody.Position;
                if (distance.magnitude < _hookSettings.MinHookDistance)
                {
                    Debug.Log("Hook ended (CalculateHookDirection)");
                    _isHooking = false;
                    HookingEnded?.Invoke();
                }

                _hookPushDirection = distance.normalized;
                yield return null;
            }
        }

        private IEnumerator RemoveHookAfterTimeOut()
        {
            yield return new WaitForSeconds(_hookSettings.Duration);
            if (_isHooking)
            {
                Debug.Log("Hook ended (RemoveHookAfterTimeOut)");
                _isHooking = false;
                HookingEnded?.Invoke();
            }
        }
    }
}