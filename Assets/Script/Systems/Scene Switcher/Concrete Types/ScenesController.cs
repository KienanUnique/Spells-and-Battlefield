using System;
using System.Collections.Generic;
using Systems.Scene_Switcher.Scene_Data;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using UnityEngine.SceneManagement;

namespace Systems.Scene_Switcher.Concrete_Types
{
    public class ScenesController : IScenesController
    {
        protected readonly IScenesSettings _settings;
        private bool _isAlreadyLoading;

        public ScenesController(IScenesSettings settings)
        {
            _settings = settings;
        }

        public event Action LoadingNextSceneStarted;
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
            LoadingNextSceneStarted?.Invoke();
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}