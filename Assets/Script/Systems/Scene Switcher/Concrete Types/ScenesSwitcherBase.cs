using Common.Abstract_Bases;
using Systems.Scene_Switcher.Scene_Data;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using UnityEngine.SceneManagement;
using Zenject;

namespace Systems.Scene_Switcher.Concrete_Types
{
    public class ScenesSwitcherBase : Singleton<InGameScenesSwitcher>
    {
        private bool _isAlreadyLoading = false;
        protected IScenesSettings _settings;

        [Inject]
        private void Construct(IScenesSettings settings)
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