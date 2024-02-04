using System;
using Common.Animator_Status_Controller;

namespace Player.Animator_Status_Checker
{
    public interface IReadonlyPlayerAnimatorStatusChecker : IReadonlyAnimatorStatusChecker
    {
        public event Action HookKeyMomentTrigger;
    }
}