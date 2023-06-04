using System;

namespace Player.Event_Invoker_For_Animations
{
    public interface IPlayerEventInvokerForAnimations
    {
        event Action CastSpellAnimationMoment;
        void InvokeUseSpellAnimationMomentStart();
    }
}