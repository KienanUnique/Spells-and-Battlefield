using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;

namespace Systems.Scene_Switcher.Concrete_Types
{
    public interface IScenesSwitcher
    {
        void LoadMainMenu();
        void LoadGameLevel(IGameLevelData levelToLoad);
        void LoadCredits();
    }
}