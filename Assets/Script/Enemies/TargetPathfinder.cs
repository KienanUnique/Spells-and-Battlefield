using System.Collections;
using Pathfinding;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Seeker))]
    public class TargetPathfinder : MonoBehaviour
    {
        [SerializeField] private float _updateDestinationCooldownSeconds;
        [SerializeField] private float _nextWaypointDistance;
        private Seeker _seeker;
        private Path _currentPath;
        private int _currentWaypointIndex;
        private Transform _cashedTransform;

        public void StartUpdatingPathForTarget(Transform target)
        {
            StopUpdatingPath();
            StartCoroutine(UpdatePathToTarget(target));
            StartCoroutine(TryUpdateCurrentWaypoint());
        }

        public void StopUpdatingPath()
        {
            StopAllCoroutines();
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

        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _cashedTransform = transform;
        }

        private IEnumerator TryUpdateCurrentWaypoint()
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();
            while (true)
            {
                if (_currentPath != null && _currentWaypointIndex < _currentPath.vectorPath.Count
                                         && Vector3.Distance(_currentPath.vectorPath[_currentWaypointIndex],
                                             transform.position) <=
                                         _nextWaypointDistance)
                {
                    _currentWaypointIndex++;
                }

                yield return waitForFixedUpdate;
            }
        }

        private IEnumerator UpdatePathToTarget(Transform target)
        {
            var waitForSeconds = new WaitForSeconds(_updateDestinationCooldownSeconds);
            while (true)
            {
                if (_seeker.IsDone())
                {
                    _seeker.StartPath(_cashedTransform.position, target.position, OnPathComplete);
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