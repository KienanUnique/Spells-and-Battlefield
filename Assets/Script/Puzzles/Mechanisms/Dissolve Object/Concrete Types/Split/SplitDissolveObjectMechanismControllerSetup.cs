using System.Collections.Generic;
using Puzzles.Mechanisms_Triggers;
using UnityEngine;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Puzzles.Mechanisms.Dissolve_Object.Concrete_Types.Split
{
    public class SplitDissolveObjectMechanismControllerSetup : DissolveObjectMechanismControllerSetupBase
    {
        [SerializeField] private List<MechanismsTriggerBase> _disappearTriggers;
        [SerializeField] private List<MechanismsTriggerBase> _appearTriggers;

        private IInitializableSplitDissolveObjectMechanismController _controller;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization
        {
            get
            {
                var list = new List<IInitializable>(_disappearTriggers);
                list.AddRange(_appearTriggers);
                return list;
            }
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableSplitDissolveObjectMechanismController>();
        }

        protected override void Initialize()
        {
            _controller.Initialize(_isEnabledAtStart, new List<IMechanismsTrigger>(_disappearTriggers),
                new List<IMechanismsTrigger>(_appearTriggers), DissolveEffectController, Colliders);
        }
    }
}