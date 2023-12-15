using System;
using UnityEngine;

namespace Common.Event_Invoker_For_Action_Animations
{
    public class EventInvokerForActionAnimations : MonoBehaviour, IEventInvokerForActionAnimations
    {
        public event Action ActionAnimationEnd;
        public event Action ActionAnimationKeyMomentTrigger;
        public event Action ActionAnimationStart;

        public void InvokeActionAnimationStart()
        {
            ActionAnimationStart?.Invoke();
        }

        public void InvokeActionAnimationKeyMomentTrigger()
        {
            ActionAnimationKeyMomentTrigger?.Invoke();
        }

        public void InvokeActionAnimationEnd()
        {
            ActionAnimationEnd?.Invoke();
        }
    }
}