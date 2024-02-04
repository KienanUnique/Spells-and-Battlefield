using System.Collections.Generic;
using System.Linq;
using Puzzles.Mechanisms.Moving_Platforms.Data_For_Creating;
using Puzzles.Mechanisms.Moving_Platforms.Settings;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using UnityEngine;

namespace Puzzles.Mechanisms.Moving_Platforms
{
    public abstract class MovingPlatformWithStickingBase : MechanismControllerBase
    {
        private IColliderTrigger _platformCollider;

        protected void Initialize(IMovingPlatformDataForControllerBase dataForControllerBase)
        {
            ParentObjectToMove = dataForControllerBase.ObjectToMove;
            _platformCollider = dataForControllerBase.PlatformCollider;
            Settings = dataForControllerBase.Settings;
            MovementSpeed = dataForControllerBase.MovementSpeed;
            Waypoints = dataForControllerBase.Waypoints;
            ParentObjectToMove.position = Waypoints.First();
            SetInitializedStatus();
        }

        protected float MovementSpeed { get; private set; }
        protected Transform ParentObjectToMove { get; private set; }
        protected IMovingPlatformsSettings Settings { get; private set; }
        protected List<Vector3> Waypoints { get; private set; }

        protected override void SubscribeOnEvents()
        {
            base.SubscribeOnEvents();
            _platformCollider.TriggerEnter += OnPlatformColliderTriggerEnter;
            _platformCollider.TriggerExit += OnPlatformColliderTriggerExit;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            _platformCollider.TriggerEnter -= OnPlatformColliderTriggerEnter;
            _platformCollider.TriggerExit -= OnPlatformColliderTriggerExit;
        }

        private void OnPlatformColliderTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IToMovingPlatformStickable stickable))
            {
                return;
            }

            stickable.StickToPlatform(ParentObjectToMove);
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