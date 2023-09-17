using System.Collections.Generic;
using Systems.Scene_Switcher.Scene_Data;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using UnityEngine.SceneManagement;

namespace Systems.Scene_Switcher.Concrete_Types
{
    public class ScenesManager : IScenesManager
    {
        protected readonly IScenesSettings _settings;
        private bool _isAlreadyLoading;

        public ScenesManager(IScenesSettings settings)
        {
            _settings = settings;
        }

        public IReadOnlyCollection<IGameLevelData> GameLevels => _settings.GameLevels;

        public void LoadMainMenu()
        {
            LoadScene(_settings.MainMenuScene);
        }

        public void LoadGameLevel(IGameLevelData levelToLoad)
        {
            LoadScene(levelToLoad);
        }

        public void LoadCredits()
        {
            LoadScene(_settings.CreditsScene);
        }

        protected void LoadScene(ISceneData sceneToLoad)
        {
            LoadScene(sceneToLoad.SceneName);
        }

        protected void LoadScene(string sceneName)
        {
            if (_isAlreadyLoading)
            {
                return;
            }

            _isAlreadyLoading = true;
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}