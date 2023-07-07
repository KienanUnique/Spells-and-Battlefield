using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Puzzles.Triggers;
using Puzzles.Triggers.Box_Collider_Trigger;
using Settings.Puzzles.Mechanisms;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platform_With_Stops
{
    [RequireComponent(typeof(MovingPlatformWithStopsControllerSetup))]
    public class MovingPlatformWithStopsController : MovingPlatformWithStickingBase,
        IInitializableMovingPlatformWithStopsController
    {
        private List<ITrigger> _moveNextTriggers;
        private List<ITrigger> _movePreviousTriggers;
        private List<Vector3> _waypoints;
        private int _currentWaypoint;
        private MovingPlatformWithStopsSettings _settings;
        private float _movementSpeed;

        public void Initialize(Transform objectToMove, List<ITrigger> moveNextTriggers,
            List<ITrigger> movePreviousTriggers, List<Vector3> waypoints, float movementSpeed,
            MovingPlatformWithStopsSettings settings, IColliderTrigger platformCollider)
        {
            base.Initialize(platformCollider, objectToMove);
            _moveNextTriggers = moveNextTriggers;
            _movePreviousTriggers = movePreviousTriggers;
            _waypoints = waypoints;
            _settings = settings;
            _movementSpeed = movementSpeed;
            SetInitializedStatus();
            _parentObjectToMove.position = waypoints.First();
        }

        protected override void SubscribeOnEvents()
        {
            foreach (var trigger in _moveNextTriggers)
            {
                trigger.Triggered += TryMoveToNextWaypoint;
            }

            foreach (var trigger in _movePreviousTriggers)
            {
                trigger.Triggered += TryMoveToPreviousWaypoint;
            }
            base.SubscribeOnEvents();
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (var trigger in _moveNextTriggers)
            {
                trigger.Triggered -= TryMoveToNextWaypoint;
            }

            foreach (var trigger in _movePreviousTriggers)
            {
                trigger.Triggered -= TryMoveToPreviousWaypoint;
            }
            base.UnsubscribeFromEvents();
        }

        private void TryMoveToNextWaypoint()
        {
            if (_currentWaypoint >= _waypoints.Count - 1) return;
            _currentWaypoint++;
            MoveToPosition(_waypoints[_currentWaypoint]);
        }

        private void TryMoveToPreviousWaypoint()
        {
            if (_currentWaypoint <= 0) return;
            _currentWaypoint--;
            MoveToPosition(_waypoints[_currentWaypoint]);
        }

        private void MoveToPosition(Vector3 positionToMove)
        {
            _parentObjectToMove.DOKill();
            _parentObjectToMove.DOMove(positionToMove, _movementSpeed).SetSpeedBased()
                .SetEase(_settings.MovementEase).SetLink(gameObject);
        }
    }
}