using System.Collections.ObjectModel;
using Systems.Scene_Switcher.Scene_Data;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;

namespace Systems.Scene_Switcher
{
    public interface IScenesSettings
    {
        public ReadOnlyCollection<IGameLevelData> GameLevels { get; }
        public ISceneData MainMenuScene { get; }
        public ISceneData CreditsScene { get; }
    }
}