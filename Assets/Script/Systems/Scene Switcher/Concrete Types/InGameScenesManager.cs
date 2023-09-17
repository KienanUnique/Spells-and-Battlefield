using System.Linq;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using UnityEngine.SceneManagement;

namespace Systems.Scene_Switcher.Concrete_Types
{
    public class InGameScenesManager : ScenesManager, IInGameSceneManager
    {
        private readonly IGameLevelData _currentLevel;
        private readonly int _currentLevelIndex;
        private readonly bool _isCurrentLevelLast;

        public InGameScenesManager(IScenesSettings settings) : base(settings)
        {
            string currentName = SceneManager.GetActiveScene().name;
            _currentLevel = _settings.GameLevels.First(level => level.SceneName == currentName);
            _currentLevelIndex = _settings.GameLevels.IndexOf(_currentLevel);
            _isCurrentLevelLast = _currentLevelIndex == _settings.GameLevels.Count - 1;
        }

        public void LoadNextLevel()
        {
            LoadScene(_isCurrentLevelLast
                ? _settings.CreditsScene
                : _settings.GameLevels.ElementAt(_currentLevelIndex + 1));
        }

        public void RestartLevel()
        {
            LoadScene(_currentLevel);
        }
    }
}