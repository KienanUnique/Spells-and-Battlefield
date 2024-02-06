using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Puzzles.Mechanisms;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Trigger_On_Mechanism_Job
{
    public class TriggerOnMechanismJobControllerSetup : MechanismsTriggerSetupBase
    {
        [SerializeField] private List<MechanismControllerBase> _mechanismsTrigger;
        [SerializeField] private MechanismJobEventType _eventType;
        private IInitializableTriggerOnMechanismJobController _controller;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableTriggerOnMechanismJobController>();
        }

        protected override void Initialize()
        {
            _controller.Initialize(_eventType, new List<IMechanismController>(_mechanismsTrigger), BaseSetupData);
        }
    }
}