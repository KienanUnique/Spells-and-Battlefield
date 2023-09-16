using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating;
using Puzzles.Mechanisms.Moving_Platforms.Settings;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms
{
    public abstract class MovingPlatformWithStickingBase : InitializableMonoBehaviourBase
    {
        protected float _delayInSeconds;
        protected bool _isTriggersDisabled;
        protected float _movementSpeed;
        protected Transform _parentObjectToMove;
        protected IMovingPlatformsSettings _settings;
        protected List<Vector3> _waypoints;
        private IColliderTrigger _platformCollider;

        protected void Initialize(IMovingPlatformDataForControllerBase dataForControllerBase)
        {
            _parentObjectToMove = dataForControllerBase.ObjectToMove;
            _platformCollider = dataForControllerBase.PlatformCollider;
            _settings = dataForControllerBase.Settings;
            _movementSpeed = dataForControllerBase.MovementSpeed;
            _waypoints = dataForControllerBase.Waypoints;
            _isTriggersDisabled = false;
            _delayInSeconds = dataForControllerBase.DelayInSeconds;
            _parentObjectToMove.position = _waypoints.First();
            SetInitializedStatus();
        }

        protected override void SubscribeOnEvents()
        {
            _platformCollider.TriggerEnter += OnPlatformColliderTriggerEnter;
            _platformCollider.TriggerExit += OnPlatformColliderTriggerExit;
        }

        protected override void UnsubscribeFromEvents()
        {
            _platformCollider.TriggerEnter -= OnPlatformColliderTriggerEnter;
            _platformCollider.TriggerExit -= OnPlatformColliderTriggerExit;
        }

        private void OnPlatformColliderTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IToMovingPlatformStickable stickable))
            {
                return;
            }

            stickable.StickToPlatform(_parentObjectToMove);
        }

        private void OnPlatformColliderTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IToMovingPlatformStickable stickable))
            {
                return;
            }

            stickable.UnstickFromPlatform();
        }
    }
}