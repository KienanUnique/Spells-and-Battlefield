using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Puzzles.Mechanisms_Triggers.Box_Collider_Trigger;
using Puzzles.Mechanisms_Triggers.Identifiers;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Trigger_Zone
{
    public class TriggerZoneControllerSetup : MechanismsTriggerSetupBase
    {
        [SerializeField] private ColliderTrigger _colliderTrigger;
        [SerializeField] private IdentifierScriptableObjectBase _identifier;
        [SerializeField] private TriggerEventType _triggerEventType;
        private IInitializableTriggerZoneController _controller;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Initialize()
        {
            _controller.Initialize(_identifier, _colliderTrigger, _triggerEventType, BaseSetupData);
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableTriggerZoneController>();
        }
    }
}