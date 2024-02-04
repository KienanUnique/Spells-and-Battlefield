using System;
using System.Collections;
using Common.Interfaces;
using Common.Readonly_Transform;
using DG.Tweening;
using UnityEngine;

namespace Player.Visual.Hook_Trail
{
    public class HookTrailVisual : IHookTrailVisual
    {
        private readonly TrailRenderer _trailRenderer;
        private readonly IReadonlyTransform _handPoint;
        private readonly IHookTrailVisualSettings _settings;
        private readonly Transform _trailTransform;
        private readonly GameObject _trailGameObject;
        private readonly ICoroutineStarter _coroutineStarter;

        private bool _needUpdateTrailPosition;

        public HookTrailVisual(TrailRenderer trailRenderer, IReadonlyTransform handPoint,
            IHookTrailVisualSettings settings, ICoroutineStarter coroutineStarter)
        {
            _trailRenderer = trailRenderer;
            _handPoint = handPoint;
            _settings = settings;
            _trailTransform = trailRenderer.transform;
            _trailGameObject = trailRenderer.gameObject;
            _coroutineStarter = coroutineStarter;
            _trailGameObject.SetActive(false);
        }

        public event Action TrailArrivedToHookPoint;

        public void MoveTrailToPoint(Vector3 hookPoint)
        {
            _trailRenderer.Clear();
            _trailTransform.position = _handPoint.Position;
            _trailRenderer.AddPositions(new[] {_handPoint.Position, _handPoint.Position});
            _trailGameObject.SetActive(true);
            _trailTransform.DOMove(hookPoint, _settings.HookTrailSpeed)
                           .SetSpeedBased()
                           .SetLink(_trailGameObject)
                           .SetEase(_settings.HookTrailEase)
                           .OnComplete(HandleArrivingToHookPoint)
                           .OnUpdate(UpdateTrailPositions);
        }

        public void Disappear()
        {
            _needUpdateTrailPosition = false;
            _trailGameObject.SetActive(false);
            _trailRenderer.Clear();
        }

        private void HandleArrivingToHookPoint()
        {
            _needUpdateTrailPosition = true;
            _coroutineStarter.StartCoroutine(UpdateTrailStartPosition());
            TrailArrivedToHookPoint?.Invoke();
        }

        private void UpdateTrailPositions()
        {
            _trailRenderer.SetPositions(new[] {_trailTransform.position, _handPoint.Position});
        }

        private IEnumerator UpdateTrailStartPosition()
        {
            while (_needUpdateTrailPosition)
            {
                _trailRenderer.SetPosition(0, _handPoint.Position);
                yield return null;
            }
        }
    }
}