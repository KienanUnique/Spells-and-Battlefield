namespace Game_Managers.Time_Controller
{
    public interface ITimeController : ITimeControllerForPlayer
    {
        public void StopTime();
        public void RestoreTimeToPrevious();
    }
}