using Systems.Scene_Switcher.Scene_Data;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using UnityEngine.SceneManagement;

namespace Systems.Scene_Switcher.Concrete_Types
{
    public class ScenesSwitcherBase
    {
        private bool _isAlreadyLoading = false;
        protected readonly IScenesSettings _settings;

        public ScenesSwitcherBase(IScenesSettings settings)
        {
            _settings = settings;
        }

        public void LoadMainMenu()
        {
            LoadScene(_settings.MainMenuScene);
        }

        public void LoadGameLevel(IGameLevelData levelToLoad)
        {
            LoadScene(levelToLoad);
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