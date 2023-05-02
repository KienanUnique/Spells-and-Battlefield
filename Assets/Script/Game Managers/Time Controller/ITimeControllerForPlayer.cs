namespace Game_Managers.Time_Controller
{
    public interface ITimeControllerForPlayer
    {
        public void SlowDownTimeForDashAiming();
        public void RestoreTimeToNormal();
    }
}