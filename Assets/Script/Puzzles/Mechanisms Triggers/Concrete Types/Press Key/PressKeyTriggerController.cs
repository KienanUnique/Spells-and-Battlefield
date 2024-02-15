using Puzzles.Mechanisms_Triggers.Concrete_Types.Press_Key.Setup;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Press_Key
{
    public class PressKeyTriggerController : MechanismsTriggerBase,
        IPressKeyInteractable,
        IInitializablePressKeyTriggerController
    {
        public void Initialize(MechanismsTriggerBaseSetupData setupData)
        {
            InitializeBase(setupData);
        }

        public void Interact()
        {
            TryInvokeTriggerEvent();
        }

        protected override void SubscribeOnEvents()
        {
        }

        protected override void UnsubscribeFromEvents()
        {
        }
    }
}