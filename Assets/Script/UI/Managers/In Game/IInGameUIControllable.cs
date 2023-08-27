namespace UI.Managers.In_Game
{
    public interface IInGameUIControllable
    {
        public void RequestQuitToMainMenu();
        public void RequestRestartLevel();
        public void RequestLoadNextLevel();
        public void TryCloseCurrentWindow();
    }
}