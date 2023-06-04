using System.Collections;
using Common;
using Common.Readonly_Transform;
using General_Settings_in_Scriptable_Objects;
using General_Settings_in_Scriptable_Objects.Sections;
using Interfaces;
using Pathfinding;
using UnityEngine;

namespace Enemies
{
    public class TargetPathfinder
    {
        private readonly Seeker _seeker;
        private readonly TargetPathfinderSettingsSection _settings;
        private readonly ICoroutineStarter _coroutineStarter;
        private readonly IReadonlyTransform _seekerObjectTransform;
        private Path _currentPath;
        private int _currentWaypointIndex;
        private Coroutine _tryUpdateCurrentWaypointCoroutine;
        private Coroutine _updatePathToTargetCoroutine;

        public TargetPathfinder(Seeker seeker, TargetPathfinderSettingsSection settings,
            ICoroutineStarter coroutineStarter)
        {
            _seeker = seeker;
            _seekerObjectTransform = new ReadonlyTransform(_seeker.transform);
            _settings = settings;
            _coroutineStarter = coroutineStarter;
        }

        public void StartUpdatingPathForTarget(IReadonlyTransform targetPosition)
        {
            StopUpdatingPath();
            _updatePathToTargetCoroutine = _coroutineStarter.StartCoroutine(UpdatePathToTarget(targetPosition));
            _tryUpdateCurrentWaypointCoroutine = _coroutineStarter.StartCoroutine(TryUpdateCurrentWaypoint());
        }

        public void StopUpdatingPath()
        {
            TryStopCoroutine(_updatePathToTargetCoroutine);
            TryStopCoroutine(_tryUpdateCurrentWaypointCoroutine);
        }

        public bool TryGetNextWaypoint(out Vector3 position)
        {
            if (_currentPath == null || _currentWaypointIndex >= _currentPath.vectorPath.Count)
            {
                position = Vector3.zero;
                return false;
            }

            position = _currentPath.vectorPath[_currentWaypointIndex];
            return true;
        }

        private void TryStopCoroutine(Coroutine coroutineToStop)
        {
            if (coroutineToStop != null)
            {
                _coroutineStarter.StopCoroutine(coroutineToStop);
            }
        }

        private IEnumerator TryUpdateCurrentWaypoint()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                if (_currentPath != null && _currentWaypointIndex < _currentPath.vectorPath.Count
                                         && Vector3.Distance(_currentPath.vectorPath[_currentWaypointIndex],
                                             _seekerObjectTransform.Position) <= _settings.NextWaypointDistance)
                {
                    _currentWaypointIndex++;
                }

                yield return waitForFixedUpdate;
            }
        }

        private IEnumerator UpdatePathToTarget(IReadonlyTransform targetPosition)
        {
            var waitForSeconds = new WaitForSeconds(_settings.UpdateDestinationCooldownSeconds);
            while (true)
            {
                if (_seeker.IsDone())
                {
                    _seeker.StartPath(_seekerObjectTransform.Position, targetPosition.Position, OnPathComplete);
                }

                yield return waitForSeconds;
            }
        }

        private void OnPathComplete(Path path)
        {
            if (!path.error)
            {
                _currentPath = path;
                _currentWaypointIndex = 0;
            }
        }
    }
}