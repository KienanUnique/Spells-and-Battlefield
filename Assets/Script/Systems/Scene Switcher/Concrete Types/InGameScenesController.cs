using System.Linq;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using UnityEngine.SceneManagement;

namespace Systems.Scene_Switcher.Concrete_Types
{
    public class InGameScenesController : ScenesController, IInGameSceneController
    {
        private readonly int _currentLevelIndex;
        private readonly bool _isCurrentLevelLast;

        public InGameScenesController(IScenesSettings settings) : base(settings)
        {
            string currentName = SceneManager.GetActiveScene().name;
            CurrentLevel = _settings.GameLevels.First(level => level.SceneName == currentName);
            _currentLevelIndex = _settings.GameLevels.IndexOf(CurrentLevel);
            _isCurrentLevelLast = _currentLevelIndex == _settings.GameLevels.Count - 1;
        }

        public IGameLevelData CurrentLevel { get; }

        public void LoadNextLevel()
        {
            LoadScene(_isCurrentLevelLast
                ? _settings.CreditsScene
                : _settings.GameLevels.ElementAt(_currentLevelIndex + 1));
        }

        public void RestartLevel()
        {
            LoadScene(CurrentLevel);
        }
    }
}