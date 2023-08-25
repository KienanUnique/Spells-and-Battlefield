using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;

namespace Systems.Scene_Switcher
{
    public interface IInGameSceneSwitcher
    {
        public void LoadNextLevel();
        public void RestartLevel();
        public void LoadMainMenu();
        public void LoadGameLevel(IGameLevelData levelToLoad);
    }
}