using System.Collections.Generic;
using Common.Collider_With_Disabling;
using Common.Dissolve_Effect_Controller;
using Puzzles.Mechanisms_Triggers;

namespace Puzzles.Mechanisms.Dissolve_Object.Concrete_Types.Split
{
    public class SplitDissolveObjectMechanismController : DissolveObjectMechanismControllerBase,
        IInitializableSplitDissolveObjectMechanismController
    {
        private List<IMechanismsTrigger> _disappearTriggers;
        private List<IMechanismsTrigger> _appearTriggers;
        private bool _isEnabled;

        public void Initialize(bool isEnabledAtStart, List<IMechanismsTrigger> disappearTriggers,
            List<IMechanismsTrigger> appearTriggers, IDissolveEffectController dissolveEffectController,
            List<IColliderWithDisabling> collidersToDisable)
        {
            _disappearTriggers = disappearTriggers;
            _appearTriggers = appearTriggers;

            _isEnabled = IsEnabled;
            InitializeBase(isEnabledAtStart, dissolveEffectController, collidersToDisable);
        }

        protected override void StartJob()
        {
            ChangeState(_isEnabled);
        }

        protected override void SubscribeOnEvents()
        {
            foreach (var trigger in _appearTriggers)
            {
                trigger.Triggered += OnAppearTriggered;
            }

            foreach (var trigger in _disappearTriggers)
            {
                trigger.Triggered += OnDisappearTriggered;
            }

            base.SubscribeOnEvents();
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (var trigger in _appearTriggers)
            {
                trigger.Triggered -= OnAppearTriggered;
            }

            foreach (var trigger in _disappearTriggers)
            {
                trigger.Triggered -= OnDisappearTriggered;
            }
            
            base.UnsubscribeFromEvents();
        }

        private void OnAppearTriggered()
        {
            if (IsBusy)
            {
                return;
            }

            _isEnabled = true;
            OnTriggered();
        }

        private void OnDisappearTriggered()
        {
            if (IsBusy)
            {
                return;
            }

            _isEnabled = false;
            OnTriggered();
        }
    }
}