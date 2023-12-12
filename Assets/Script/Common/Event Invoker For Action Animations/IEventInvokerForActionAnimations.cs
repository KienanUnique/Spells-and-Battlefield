using System;

namespace Common.Event_Invoker_For_Action_Animations
{
    public interface IEventInvokerForActionAnimations
    {
        public event Action ActionAnimationKeyMomentTrigger;
        public event Action ActionAnimationStart;
    }
}