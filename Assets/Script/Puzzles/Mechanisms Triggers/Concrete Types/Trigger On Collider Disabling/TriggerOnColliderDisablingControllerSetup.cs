using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Collider_With_Disabling;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Trigger_On_Collider_Disabling
{
    public class TriggerOnColliderDisablingControllerSetup : MechanismsTriggerSetupBase
    {
        [SerializeField] private ColliderWithDisabling _colliderWithDisabling;
        private IInitializableTriggerOnColliderDisablingController _controller;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableTriggerOnColliderDisablingController>();
        }

        protected override void Initialize()
        {
            _controller.Initialize(_colliderWithDisabling, BaseSetupData);
        }
    }
}