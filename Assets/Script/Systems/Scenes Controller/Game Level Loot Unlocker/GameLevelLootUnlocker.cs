using System.Collections.ObjectModel;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;

namespace Systems.Scenes_Controller.Game_Level_Loot_Unlocker
{
    public class GameLevelLootUnlocker : IGameLevelLootUnlocker
    {
        private readonly ReadOnlyCollection<IGameLevelData> _gameLevels;
        private readonly int _currentGameLevelIndex;

        public GameLevelLootUnlocker(int currentGameLevelIndex, ReadOnlyCollection<IGameLevelData> gameLevels)
        {
            _currentGameLevelIndex = currentGameLevelIndex;
            _gameLevels = gameLevels;
        }

        public bool IsUnlockedOnCurrentLevel(IGameLevelData inclusiveRequiredLevelToUnlock)
        {
            return _currentGameLevelIndex >= _gameLevels.IndexOf(inclusiveRequiredLevelToUnlock);
        }
    }
}