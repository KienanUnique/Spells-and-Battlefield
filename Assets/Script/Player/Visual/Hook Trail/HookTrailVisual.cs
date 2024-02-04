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
        private const int TrailPositionsCount = 2;

        private readonly TrailRenderer _trailRenderer;
        private readonly IReadonlyTransform _handPoint;
        private readonly IHookTrailVisualSettings _settings;
        private readonly Transform _trailTransform;
        private readonly GameObject _trailGameObject;
        private readonly ICoroutineStarter _coroutineStarter;
        private readonly Vector3[] _trailPositions = new Vector3[TrailPositionsCount];

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
            _trailPositions[0] = _handPoint.Position;
            _trailPositions[1] = _handPoint.Position;
            _trailRenderer.AddPositions(_trailPositions);
            _trailGameObject.SetActive(true);
            _trailTransform.DOMove(hookPoint, _settings.HookTrailSpeed)
                           .SetSpeedBased()
                           .SetLink(_trailGameObject)
                           .OnComplete(HandleArrivingToHookPoint)
                           .OnUpdate(UpdateTrailPositions);
        }

        public void Disappear()
        {
            _needUpdateTrailPosition = false;
            _trailGameObject.SetActive(false);
            _trailRenderer.Clear();
            _trailTransform.DOKill();
        }

        private void HandleArrivingToHookPoint()
        {
            _needUpdateTrailPosition = true;
            _coroutineStarter.StartCoroutine(UpdateTrailPositionsContinuously());
            TrailArrivedToHookPoint?.Invoke();
        }

        private void UpdateTrailPositions()
        {
            _trailPositions[0] = _trailTransform.position;
            _trailPositions[1] = _handPoint.Position;
            _trailRenderer.SetPositions(_trailPositions);
        }

        private IEnumerator UpdateTrailPositionsContinuously()
        {
            while (_needUpdateTrailPosition)
            {
                UpdateTrailPositions();
                yield return null;
            }
        }
    }
}