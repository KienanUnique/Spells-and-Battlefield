using System;
using UnityEngine;

namespace Player.Event_Invoker_For_Animations
{
    public class PlayerEventInvokerForAnimations : MonoBehaviour, IPlayerEventInvokerForAnimations
    {
        public event Action CastSpellAnimationMoment;
        public void InvokeUseSpellAnimationMomentStart() => CastSpellAnimationMoment?.Invoke();
    }
}