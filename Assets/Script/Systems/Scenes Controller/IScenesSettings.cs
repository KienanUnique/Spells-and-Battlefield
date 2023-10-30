using System.Collections.ObjectModel;
using Systems.Scenes_Controller.Scene_Data;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;

namespace Systems.Scenes_Controller
{
    public interface IScenesSettings
    {
        public ReadOnlyCollection<IGameLevelData> GameLevels { get; }
        public ISceneData MainMenuScene { get; }
        public ISceneData CreditsScene { get; }
        public ISceneData ComicsCutsceneScene { get; }
    }
}