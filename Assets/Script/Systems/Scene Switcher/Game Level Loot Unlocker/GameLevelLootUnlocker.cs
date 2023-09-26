using ModestTree;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;

namespace Systems.Scene_Switcher.Current_Game_Level_Information
{
    public class GameLevelLootUnlocker : IGameLevelLootUnlocker
    {
        private readonly IGameLevelData[] _gameLevels;
        private readonly int _currentGameLevelIndex;

        public GameLevelLootUnlocker(IGameLevelData currentGameLevel, IGameLevelData[] gameLevels)
        {
            _gameLevels = gameLevels;
            _currentGameLevelIndex = _gameLevels.IndexOf(currentGameLevel);
        }

        public bool IsUnlockedOnCurrentLevel(IGameLevelData inclusiveRequiredLevelToUnlock)
        {
            return _currentGameLevelIndex >= _gameLevels.IndexOf(inclusiveRequiredLevelToUnlock);
        }
    }
}