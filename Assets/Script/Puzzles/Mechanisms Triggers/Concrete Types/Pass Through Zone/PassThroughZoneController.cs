using System;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Puzzles.Mechanisms_Triggers.Identifiers;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Pass_Through_Zone
{
    public class PassThroughZoneController : MechanismsTriggerBase, IInitializablePassThroughZoneController
    {
        private IColliderTrigger _colliderTrigger;
        private IIdentifier _identifier;

        public void Initialize(IIdentifier identifier, IColliderTrigger colliderTrigger)
        {
            _identifier = identifier;
            _colliderTrigger = colliderTrigger;
            SetInitializedStatus();
        }

        public override event Action Triggered;

        protected override void SubscribeOnEvents()
        {
            _colliderTrigger.TriggerExit += OnTriggerExit;
        }

        protected override void UnsubscribeFromEvents()
        {
            _colliderTrigger.TriggerExit -= OnTriggerExit;
        }

        private void OnTriggerExit(Collider obj)
        {
            if (_identifier.IsObjectOfRequiredType(obj))
            {
                Triggered?.Invoke();
            }
        }
    }
}