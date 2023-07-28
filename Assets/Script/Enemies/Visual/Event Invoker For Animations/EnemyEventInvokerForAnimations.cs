using System;
using UnityEngine;

namespace Enemies.Visual.Event_Invoker_For_Animations
{
    public class EnemyEventInvokerForAnimations : MonoBehaviour, IEnemyEventInvokerForAnimations
    {
        public event Action AnimationUseActionMomentTrigger;
        public void InvokeAnimationUseActionMomentEvent() => AnimationUseActionMomentTrigger?.Invoke();
    }
}