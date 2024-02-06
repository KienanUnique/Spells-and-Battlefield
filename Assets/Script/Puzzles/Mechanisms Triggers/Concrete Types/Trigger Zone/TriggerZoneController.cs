using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Puzzles.Mechanisms_Triggers.Identifiers;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Trigger_Zone
{
    public class TriggerZoneController : MechanismsTriggerBase, IInitializableTriggerZoneController
    {
        private IColliderTrigger _colliderTrigger;
        private IIdentifier _identifier;
        private TriggerZoneEventType _eventType;

        public void Initialize(IIdentifier identifier, IColliderTrigger colliderTrigger, TriggerZoneEventType eventType,
            MechanismsTriggerBaseSetupData baseSetupData)
        {
            _identifier = identifier;
            _colliderTrigger = colliderTrigger;
            _eventType = eventType;
            InitializeBase(baseSetupData);
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

        private void OnTriggerEnter(Collider obj)
        {
            if ((_eventType == TriggerZoneEventType.Both || _eventType == TriggerZoneEventType.Enter) &&
                _identifier.IsObjectOfRequiredType(obj))
            {
                TryInvokeTriggerEvent();
            }
        }

        private void OnTriggerExit(Collider obj)
        {
            if ((_eventType == TriggerZoneEventType.Both || _eventType == TriggerZoneEventType.Exit) &&
                _identifier.IsObjectOfRequiredType(obj))
            {
                TryInvokeTriggerEvent();
            }
        }
    }
}