namespace Game_Managers.Time_Controller
{
    public interface ITimeController
    {
        public void StopTime();
        public void RestoreTimeToPrevious();
        public void RestoreTimeToNormal();
    }
}