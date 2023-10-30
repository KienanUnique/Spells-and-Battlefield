using Systems.Scenes_Controller.Game_Level_Loot_Unlocker;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;
using Systems.Score;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Data;

namespace Systems.Scenes_Controller.Game_Level_Information_Storage
{
    public interface IGameLevelInformationStorage
    {
        public IComicsData StoredLevelComicsData { get; }
        public IGameLevelStatistic StoredLevelStatistic { get; }
        public IGameLevelData NextLevel { get; }
        public IGameLevelData StoredLevelData { get; }
        public IGameLevelLootUnlocker CurrentGameLevelLootUnlocker { get; }
        public bool IsCurrentLevelLast { get; }
        public void RememberLevel(IGameLevelData levelData);
        public void RememberLevelStatistic(IGameLevelStatistic levelStatistic);
        public void ForgetAllInformation();
    }
}