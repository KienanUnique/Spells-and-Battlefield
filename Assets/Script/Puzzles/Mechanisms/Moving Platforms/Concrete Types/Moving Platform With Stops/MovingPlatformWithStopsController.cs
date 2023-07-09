using System.Collections.Generic;
using DG.Tweening;
using Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating;
using Puzzles.Triggers;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms.Concrete_Types.Moving_Platform_With_Stops
{
    [RequireComponent(typeof(MovingPlatformWithStopsControllerSetup))]
    public class MovingPlatformWithStopsController : MovingPlatformWithStickingBase,
        IInitializableMovingPlatformWithStopsController
    {
        private List<ITrigger> _moveNextTriggers;
        private List<ITrigger> _movePreviousTriggers;
        private int _currentWaypoint;

        public void Initialize(List<ITrigger> moveNextTriggers, List<ITrigger> movePreviousTriggers,
            IMovingPlatformDataForControllerBase dataForControllerBase)
        {
            _moveNextTriggers = moveNextTriggers;
            _movePreviousTriggers = movePreviousTriggers;
            _currentWaypoint = 0;
            base.Initialize(dataForControllerBase);
            SetInitializedStatus();
        }

        private int LastWaypointIndex => _waypoints.Count - 1;

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            foreach (var trigger in _moveNextTriggers)
            {
                trigger.Triggered += TryMoveToNextWaypoint;
            }

            foreach (var trigger in _movePreviousTriggers)
            {
                trigger.Triggered += TryMoveToPreviousWaypoint;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            foreach (var trigger in _moveNextTriggers)
            {
                trigger.Triggered -= TryMoveToNextWaypoint;
            }

            foreach (var trigger in _movePreviousTriggers)
            {
                trigger.Triggered -= TryMoveToPreviousWaypoint;
            }
        }

        private void TryMoveToNextWaypoint()
        {
            if (_isTriggersDisabled || _currentWaypoint >= LastWaypointIndex) return;
            _currentWaypoint++;
            MoveToPosition(_waypoints[_currentWaypoint]);
        }

        private void TryMoveToPreviousWaypoint()
        {
            if (_isTriggersDisabled || _currentWaypoint <= 0) return;
            _currentWaypoint--;
            MoveToPosition(_waypoints[_currentWaypoint]);
        }

        private void MoveToPosition(Vector3 waypoint)
        {
            _isTriggersDisabled = true;
            _parentObjectToMove.DOKill();
            _parentObjectToMove.DOMove(waypoint, _movementSpeed)
                .ApplyCustomSetupForMovingPlatforms(gameObject, _settings, _delayInSeconds)
                .OnKill(() => _isTriggersDisabled = false);
        }
    }
}