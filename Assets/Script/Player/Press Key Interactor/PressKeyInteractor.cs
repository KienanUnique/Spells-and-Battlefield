using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using Common.Readonly_Transform;
using ModestTree;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Puzzles.Mechanisms_Triggers.Concrete_Types.Press_Key;
using UnityEngine;

namespace Player.Press_Key_Interactor
{
    public class PressKeyInteractor : BaseWithDisabling, IPressKeyInteractor
    {
        private readonly IReadonlyTransform _thisTransform;
        private readonly IColliderTrigger _colliderTrigger;
        private readonly List<Collider> _colliders = new();

        public PressKeyInteractor(IReadonlyTransform thisTransform, IColliderTrigger colliderTrigger)
        {
            _thisTransform = thisTransform;
            _colliderTrigger = colliderTrigger;
        }

        public event Action CanInteractNow;
        public event Action CanNotInteractNow;
        public bool CanInteract => !_colliders.IsEmpty();

        public void TryInteract()
        {
            if (!CanInteract)
            {
                return;
            }

            var closestInteractable = GetClosestInteractable();
            closestInteractable.Interact();
        }

        protected override void SubscribeOnEvents()
        {
            _colliderTrigger.TriggerEnter += OnTriggerEnter;
            _colliderTrigger.TriggerExit += OnTriggerExit;
        }

        protected override void UnsubscribeFromEvents()
        {
            _colliderTrigger.TriggerEnter -= OnTriggerEnter;
            _colliderTrigger.TriggerExit -= OnTriggerExit;
        }

        private void OnTriggerExit(Collider obj)
        {
            _colliders.Remove(obj);

            if (_colliders.IsEmpty())
            {
                CanNotInteractNow?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider obj)
        {
            _colliders.Add(obj);

            if (_colliders.Count == 1)
            {
                CanInteractNow?.Invoke();
            }
        }

        private IPressKeyInteractable GetClosestInteractable()
        {
            var thisPosition = _thisTransform.Position;
            var closestDistance = float.PositiveInfinity;

            IPressKeyInteractable closestInteractable = null;
            foreach (var collider in _colliders)
            {
                var distance = (collider.transform.position - thisPosition).sqrMagnitude;
                if (!(distance < closestDistance))
                {
                    continue;
                }

                closestDistance = distance;
                closestInteractable = collider.GetComponent<IPressKeyInteractable>();
            }

            return closestInteractable;
        }
    }
}