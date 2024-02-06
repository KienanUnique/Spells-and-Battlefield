using Common.Collider_With_Disabling;
using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Trigger_On_Collider_Disabling
{
    public class TriggerOnColliderDisablingController : MechanismsTriggerBase,
        IInitializableTriggerOnColliderDisablingController
    {
        private IReadonlyColliderWithDisabling _colliderWithDisabling;

        public void Initialize(IReadonlyColliderWithDisabling colliderWithDisabling,
            MechanismsTriggerBaseSetupData setupData)
        {
            _colliderWithDisabling = colliderWithDisabling;
            InitializeBase(setupData);
        }

        protected override void SubscribeOnEvents()
        {
            _colliderWithDisabling.Disabled += OnDisabled;
        }

        protected override void UnsubscribeFromEvents()
        {
            _colliderWithDisabling.Disabled -= OnDisabled;
        }

        private void OnDisabled(IReadonlyColliderWithDisabling arg1, Collider arg2)
        {
            TryInvokeTriggerEvent();
        }
    }
}