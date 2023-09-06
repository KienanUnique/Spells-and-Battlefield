using System.Collections.Generic;
using DG.Tweening;
using Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms.Concrete_Types.Moving_Platform_With_Stops
{
    [RequireComponent(typeof(MovingPlatformWithStopsControllerSetup))]
    public class MovingPlatformWithStopsController : MovingPlatformWithStickingBase,
        IInitializableMovingPlatformWithStopsController
    {
        private int _currentWaypoint;
        private List<IMechanismsTrigger> _moveNextTriggers;
        private List<IMechanismsTrigger> _movePreviousTriggers;

        public void Initialize(List<IMechanismsTrigger> moveNextTriggers, List<IMechanismsTrigger> movePreviousTriggers,
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
            foreach (IMechanismsTrigger trigger in _moveNextTriggers)
            {
                trigger.Triggered += TryMoveToNextWaypoint;
            }

            foreach (IMechanismsTrigger trigger in _movePreviousTriggers)
            {
                trigger.Triggered += TryMoveToPreviousWaypoint;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            foreach (IMechanismsTrigger trigger in _moveNextTriggers)
            {
                trigger.Triggered -= TryMoveToNextWaypoint;
            }

            foreach (IMechanismsTrigger trigger in _movePreviousTriggers)
            {
                trigger.Triggered -= TryMoveToPreviousWaypoint;
            }
        }

        private void TryMoveToNextWaypoint()
        {
            if (_isTriggersDisabled || _currentWaypoint >= LastWaypointIndex)
            {
                return;
            }

            _currentWaypoint++;
            MoveToPosition(_waypoints[_currentWaypoint]);
        }

        private void TryMoveToPreviousWaypoint()
        {
            if (_isTriggersDisabled || _currentWaypoint <= 0)
            {
                return;
            }

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