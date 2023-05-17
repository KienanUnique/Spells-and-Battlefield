using System;
using UnityEngine;

namespace Player
{
    public class PlayerEventInvokerForAnimations : MonoBehaviour
    {
        public event Action CastSpellAnimationMoment;
        public void InvokeUseSpellAnimationMomentStart() => CastSpellAnimationMoment?.Invoke();
    }
}