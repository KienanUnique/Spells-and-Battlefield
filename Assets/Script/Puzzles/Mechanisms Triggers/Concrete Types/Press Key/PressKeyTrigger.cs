using UnityEngine;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Press_Key
{
    public class PressKeyTrigger : MechanismsTriggerBase, IPressKeyInteractable
    {
        public void Interact()
        {
            Debug.Log("TryInvokeTriggerEvent");
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