using System;
using System.Collections;
using Common.Interfaces;
using Common.Readonly_Transform;
using Player.Look;
using Player.Movement.Hooker.Point_Provider;
using Player.Movement.Hooker.Settings;
using UnityEngine;

namespace Player.Movement.Hooker
{
    public class PlayerHooker : IPlayerHooker
    {
        private const int MaxHookColliders = 1;

        private readonly IReadonlyTransform _rigidbody;
        private readonly IReadonlyPlayerLook _look;
        private readonly IPlayerHookerSettings _hookSettings;
        private readonly ICoroutineStarter _coroutineStarter;
        private readonly Collider[] _overlapResults = new Collider[MaxHookColliders];

        private Coroutine _removeHookAfterTimeOutCoroutine;

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
        public Vector3 HookPoint { get; private set; }
        public Vector3 AfterHookPushDirection { get; private set; }
        public bool IsHooking { get; private set; }

        public bool TrySetHookPoint()
        {
            if (!_look.TryCalculateLookPointWithMaxDistance(out var lookPoint, _hookSettings.MaxDistance))
            {
                return false;
            }

            var size = Physics.OverlapSphereNonAlloc(lookPoint, _hookSettings.PointSelectionRadius, _overlapResults,
                _hookSettings.Mask, QueryTriggerInteraction.Ignore);

            IHookPointProvider pointProvider = null;
            for (var i = 0; i < size; i++)
            {
                if (_overlapResults[i].TryGetComponent(out pointProvider))
                {
                    break;
                }
            }

            if (pointProvider == null)
            {
                return false;
            }

            HookPoint = pointProvider.HookPoint;
            var direction = HookPoint - _rigidbody.Position;
            AfterHookPushDirection = direction.normalized;
            return true;
        }

        public void StartCalculatingHookDirection()
        {
            IsHooking = true;
            _removeHookAfterTimeOutCoroutine = _coroutineStarter.StartCoroutine(RemoveHookAfterTimeOut());
            _coroutineStarter.StartCoroutine(CalculateHookDirection());
        }

        private IEnumerator CalculateHookDirection()
        {
            while (IsHooking)
            {
                var distance = HookPoint - _rigidbody.Position;
                var distanceMagnitude = distance.magnitude;
                var isTooClose = distanceMagnitude < _hookSettings.CancelHookDistance;
                var isAngleTooBig = Vector3.Angle(distance, AfterHookPushDirection) >=
                                    _hookSettings.StartAndCurrentDirectionsMaxAngle;
                if (isTooClose || isAngleTooBig)
                {
                    EndHooking();
                }

                HookPushDirection = distance.normalized;
                yield return null;
            }
        }

        private IEnumerator RemoveHookAfterTimeOut()
        {
            yield return new WaitForSeconds(_hookSettings.MaxDurationInSeconds);
            if (IsHooking)
            {
                EndHooking();
            }
        }

        private void EndHooking()
        {
            if (!IsHooking)
            {
                return;
            }

            _coroutineStarter.StopCoroutine(_removeHookAfterTimeOutCoroutine);
            _removeHookAfterTimeOutCoroutine = null;
            IsHooking = false;
            HookingEnded?.Invoke();
        }
    }
}