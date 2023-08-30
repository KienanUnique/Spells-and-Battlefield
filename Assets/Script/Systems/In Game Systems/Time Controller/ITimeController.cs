namespace Systems.In_Game_Systems.Time_Controller
{
    public interface ITimeController
    {
        public void StopTime();
        public void RestoreTimeToPrevious();
        public void RestoreTimeToNormal();
    }
}