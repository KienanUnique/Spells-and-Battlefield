using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Interfaces;
using Puzzles.Triggers.Box_Collider_Trigger;
using UnityEngine;

namespace Puzzles.Mechanisms
{
    public class MovingPlatformWithStickingBase : InitializableMonoBehaviourBase
    {
        protected Transform _parentObjectToMove;
        private IColliderTrigger _platformCollider;

        protected void Initialize(IColliderTrigger platformCollider, Transform objectToMove)
        {
            _parentObjectToMove = objectToMove;
            _platformCollider = platformCollider;
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
            if (!other.TryGetComponent<IToMovingPlatformStickable>(out var stickable)) return;
            stickable.StickToPlatform(_parentObjectToMove);
        }

        private void OnPlatformColliderTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<IToMovingPlatformStickable>(out var stickable)) return;
            stickable.UnstickFromPlatform();
        }
    }
}