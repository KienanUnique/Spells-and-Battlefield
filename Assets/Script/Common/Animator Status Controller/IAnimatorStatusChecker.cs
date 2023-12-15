namespace Common.Animator_Status_Controller
{
    public interface IAnimatorStatusChecker : IReadonlyAnimatorStatusChecker
    {
        public void StartChecking();
        public void StopChecking();
        public void HandleActionAnimationPlay();
        public void HandleActionAnimationCancel();
    }
}