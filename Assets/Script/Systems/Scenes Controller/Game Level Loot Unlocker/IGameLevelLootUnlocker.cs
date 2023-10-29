using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;

namespace Systems.Scenes_Controller.Game_Level_Loot_Unlocker
{
    public interface IGameLevelLootUnlocker
    {
        public bool IsUnlockedOnCurrentLevel(IGameLevelData inclusiveRequiredLevelToUnlock);
    }
}