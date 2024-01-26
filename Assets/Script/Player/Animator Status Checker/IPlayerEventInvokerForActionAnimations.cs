using System;

namespace Player.Animator_Status_Checker
{
    public interface IPlayerEventInvokerForActionAnimations
    {
        public event Action HookKeyMomentTrigger;
    }
}