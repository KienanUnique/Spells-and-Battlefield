using System;
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
        private MoveDirection _needMoveDirection;

        public void Initialize(List<IMechanismsTrigger> moveNextTriggers, List<IMechanismsTrigger> movePreviousTriggers,
            IMovingPlatformDataForControllerBase dataForControllerBase)
        {
            _moveNextTriggers = moveNextTriggers;
            _movePreviousTriggers = movePreviousTriggers;
            _currentWaypoint = 0;
            AddTriggers(_moveNextTriggers);
            AddTriggers(_movePreviousTriggers);
            base.Initialize(dataForControllerBase);
            SetInitializedStatus();
        }

        private enum MoveDirection
        {
            Next, Previous
        }

        private int LastWaypointIndex => Waypoints.Count - 1;

        protected override void StartJob()
        {
            switch (_needMoveDirection)
            {
                case MoveDirection.Next when _currentWaypoint < LastWaypointIndex:
                    _currentWaypoint++;
                    MoveToPosition(Waypoints[_currentWaypoint]);
                    break;
                case MoveDirection.Previous when _currentWaypoint > 0:
                    _currentWaypoint--;
                    MoveToPosition(Waypoints[_currentWaypoint]);
                    break;
            }
        }

        protected override void SubscribeOnEvents()
        {
            foreach (IMechanismsTrigger trigger in _moveNextTriggers)
            {
                trigger.Triggered += SetNextMovementDirection;
            }

            foreach (IMechanismsTrigger trigger in _movePreviousTriggers)
            {
                trigger.Triggered += SetPreviousMovementDirection;
            }

            base.SubscribeOnEvents();
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (IMechanismsTrigger trigger in _moveNextTriggers)
            {
                trigger.Triggered -= SetNextMovementDirection;
            }

            foreach (IMechanismsTrigger trigger in _movePreviousTriggers)
            {
                trigger.Triggered -= SetPreviousMovementDirection;
            }

            base.UnsubscribeFromEvents();
        }

        private void SetNextMovementDirection()
        {
            _needMoveDirection = MoveDirection.Next;
        }

        private void SetPreviousMovementDirection()
        {
            _needMoveDirection = MoveDirection.Previous;
        }

        private void MoveToPosition(Vector3 waypoint)
        {
            ParentObjectToMove.DOKill();
            ParentObjectToMove.DOMove(waypoint, MovementSpeed)
                              .SetSpeedBased()
                              .SetEase(Settings.MovementEase)
                              .SetLink(gameObject)
                              .OnKill(HandleDoneJob);
        }
    }
}