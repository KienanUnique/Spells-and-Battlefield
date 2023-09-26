using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;

namespace Systems.Scene_Switcher.Current_Game_Level_Information
{
    public interface IGameLevelLootUnlocker
    {
        public bool IsUnlockedOnCurrentLevel(IGameLevelData inclusiveRequiredLevelToUnlock);
    }
}