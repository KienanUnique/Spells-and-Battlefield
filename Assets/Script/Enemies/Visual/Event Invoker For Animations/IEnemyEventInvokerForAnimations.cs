using System;

namespace Enemies.Visual.Event_Invoker_For_Animations
{
    public interface IEnemyEventInvokerForAnimations
    {
        event Action AnimationUseActionMomentTrigger;
    }
}