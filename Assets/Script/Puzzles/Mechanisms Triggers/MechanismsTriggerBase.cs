using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;

namespace Puzzles.Mechanisms_Triggers
{
    public abstract class MechanismsTriggerBase : InitializableMonoBehaviourBase, IMechanismsTrigger
    {
        public event Action Triggered;
        protected abstract bool NeedTriggerOneTime { get; }
        protected bool WasTriggered { private set; get; } = false;

        protected void TryInvokeTriggerEvent()
        {
            if (NeedTriggerOneTime && WasTriggered)
            {
                return;
            }

            WasTriggered = true;
            Triggered?.Invoke();
        }
    }
}