using System.Collections.Generic;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;

namespace Puzzles.Mechanisms.Dissolve_Object.Concrete_Types.Default
{
    public class DissolveObjectMechanismControllerSetup : DissolveObjectMechanismControllerSetupBase
    {
        [SerializeField] private List<MechanismsTriggerBase> _triggers;

        private IInitializableDissolveObjectMechanismController _controller;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(_triggers);

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableDissolveObjectMechanismController>();
        }

        protected override void Initialize()
        {
            _controller.Initialize(_isEnabledAtStart, new List<IMechanismsTrigger>(_triggers), DissolveEffectController,
                Colliders);
        }
    }
}