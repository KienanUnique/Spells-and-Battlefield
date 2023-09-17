using System.Collections.Generic;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;

namespace Systems.Scene_Switcher.Concrete_Types
{
    public interface IScenesManager
    {
        public IReadOnlyCollection<IGameLevelData> GameLevels { get; }
        public void LoadMainMenu();
        public void LoadGameLevel(IGameLevelData levelToLoad);
        public void LoadCredits();
    }
}