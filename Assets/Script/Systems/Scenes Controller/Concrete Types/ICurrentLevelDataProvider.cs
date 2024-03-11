using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;

namespace Systems.Scenes_Controller.Concrete_Types
{
    public interface ICurrentLevelDataProvider
    {
        public IGameLevelData CurrentLevel { get; }
    }
}