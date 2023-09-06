using System;
using System.Collections;
using Common.Readonly_Transform;
using Enemies.Target_Pathfinder.Settings;
using Enemies.Target_Pathfinder.Setup_Data;
using Interfaces;
using ModestTree;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Target_Pathfinder
{
    public class TargetPathfinder : ITargetPathfinder
    {
        private const int CanBePassedAreasNavMeshMask = NavMesh.AllAreas;
        private readonly ICoroutineStarter _coroutineStarter;
        private readonly ITargetPathfinderSettings _settings;
        private readonly IReadonlyTransform _thisPosition;
        private Vector3[] _currentPathCorners;
        private int _currentWaypointIndex;
        private Coroutine _tryUpdateCurrentWaypointCoroutine;
        private Coroutine _updatePathToTargetCoroutine;

        public TargetPathfinder(ITargetPathfinderSetupData setupData, ITargetPathfinderSettings settings)
        {
            _thisPosition = setupData.ThisPosition;
            _settings = settings;
            _coroutineStarter = setupData.CoroutineStarter;
            _currentPathCorners = Array.Empty<Vector3>();
        }

        public Vector3 CurrentWaypoint => _currentPathCorners[_currentWaypointIndex];

        public void StartUpdatingPathForKeepingTransformOnDistance(IReadonlyTransform targetPosition,
            float needDistance)
        {
            StopUpdatingPath();
            _updatePathToTargetCoroutine =
                _coroutineStarter.StartCoroutine(UpdatePathToKeepTransformOnDistance(targetPosition, needDistance));
            _tryUpdateCurrentWaypointCoroutine = _coroutineStarter.StartCoroutine(TryUpdateCurrentWaypoint());
        }

        public void StopUpdatingPath()
        {
            TryStopCoroutine(_updatePathToTargetCoroutine);
            TryStopCoroutine(_tryUpdateCurrentWaypointCoroutine);
        }

        public bool IsPathComplete()
        {
            return _currentPathCorners.IsEmpty() || _currentWaypointIndex >= _currentPathCorners.Length;
        }

        private IEnumerator TryUpdateCurrentWaypoint()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                if (_currentPathCorners.Length > 0 &&
                    _currentWaypointIndex < _currentPathCorners.Length &&
                    Vector3.Distance(_currentPathCorners[_currentWaypointIndex], _thisPosition.Position) <=
                    _settings.NextWaypointDistance)
                {
                    _currentWaypointIndex++;
                }

                yield return waitForFixedUpdate;
            }
        }

        private IEnumerator UpdatePathToKeepTransformOnDistance(IReadonlyTransform target, float needDistance)
        {
            var waitForSeconds = new WaitForSeconds(_settings.UpdateDestinationCooldownSeconds);
            var path = new NavMeshPath();
            while (true)
            {
                NavMesh.CalculatePath(_thisPosition.Position, CalculateNeedPosition(target, needDistance),
                    CanBePassedAreasNavMeshMask, path);
                _currentPathCorners = path.corners;
                _currentWaypointIndex = 0;

                yield return waitForSeconds;
            }
        }

        private Vector3 CalculateNeedPosition(IReadonlyTransform target, float needDistance)
        {
            Vector3 offsetVector = (_thisPosition.Position - target.Position).normalized * needDistance;
            return NavMesh.SamplePosition(target.Position + offsetVector, out NavMeshHit hit,
                _settings.MaxDistanceFromTargetToNavMesh, CanBePassedAreasNavMeshMask)
                ? hit.position
                : target.Position + offsetVector;
        }

        private void TryStopCoroutine(Coroutine coroutineToStop)
        {
            if (coroutineToStop != null)
            {
                _coroutineStarter.StopCoroutine(coroutineToStop);
            }
        }
    }
}