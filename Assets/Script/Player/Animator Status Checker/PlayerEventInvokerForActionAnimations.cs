using System;
using UnityEngine;

namespace Player.Animator_Status_Checker
{
    public class PlayerEventInvokerForActionAnimations : MonoBehaviour, IPlayerEventInvokerForActionAnimations
    {
        public event Action HookKeyMomentTrigger;

        public void InvokeHookKeyMomentTrigger()
        {
            HookKeyMomentTrigger?.Invoke();
        }
    }
}