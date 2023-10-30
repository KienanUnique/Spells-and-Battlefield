using System;
using System.Collections.Generic;
using System.Linq;
using Systems.Scenes_Controller.Game_Level_Information_Storage;
using Systems.Scenes_Controller.Game_Level_Loot_Unlocker;
using Systems.Scenes_Controller.Scene_Data;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Data;
using UnityEngine.SceneManagement;

namespace Systems.Scenes_Controller.Concrete_Types
{
    public class ScenesController : IInGameSceneController, IComicsToShowProvider
    {
        private readonly IScenesSettings _settings;
        private readonly IGameLevelInformationStorage _gameLevelInformationStorage;
        private bool _isAlreadyLoading;

        public ScenesController(IScenesSettings settings)
        {
            _settings = settings;
            _gameLevelInformationStorage = new GameLevelInformationStorage(settings.GameLevels);
            string currentSceneName = SceneManager.GetActiveScene().name;
            IGameLevelData currentSceneAsGameLevel =
                _settings.GameLevels.FirstOrDefault(level => level.SceneName == currentSceneName);
            if (currentSceneAsGameLevel != default)
            {
                _gameLevelInformationStorage.RememberLevel(currentSceneAsGameLevel);
            }

            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        public event Action LoadingNextSceneStarted;

        public IGameLevelLootUnlocker CurrentGameLevelLootUnlocker =>
            _gameLevelInformationStorage.CurrentGameLevelLootUnlocker;

        public IReadOnlyCollection<IGameLevelData> GameLevels => _settings.GameLevels;
        public IComicsData ComicsToShow => _gameLevelInformationStorage.StoredLevelComicsData;

        public void LoadNextGameLevel()
        {
            if (_gameLevelInformationStorage.IsCurrentLevelLast)
            {
                LoadCredits();
            }
            else
            {
                LoadGameLevel(_gameLevelInformationStorage.NextLevel);
            }
        }

        public void LoadComicsCutscene()
        {
            if (_isAlreadyLoading)
            {
                return;
            }

            LoadScene(_settings.ComicsCutsceneScene);
        }

        public void RestartCurrentGameLevel()
        {
            LoadGameLevel(_gameLevelInformationStorage.StoredLevelData);
        }

        public void LoadMainMenu()
        {
            if (_isAlreadyLoading)
            {
                return;
            }

            _gameLevelInformationStorage.ForgetAllInformation();
            LoadScene(_settings.MainMenuScene);
        }

        public void LoadGameLevel(IGameLevelData levelToLoad)
        {
            if (_isAlreadyLoading)
            {
                return;
            }

            _gameLevelInformationStorage.RememberLevel(levelToLoad);
            LoadScene(levelToLoad);
        }

        public void LoadCredits()
        {
            if (_isAlreadyLoading)
            {
                return;
            }

            _gameLevelInformationStorage.ForgetAllInformation();
            LoadScene(_settings.CreditsScene);
        }

        private void LoadScene(ISceneData sceneToLoad)
        {
            if (_isAlreadyLoading)
            {
                throw new InvalidOperationException("Scene is already loading");
            }

            _isAlreadyLoading = true;
            LoadingNextSceneStarted?.Invoke();
            SceneManager.LoadSceneAsync(sceneToLoad.SceneName);
        }

        private void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            _isAlreadyLoading = false;
        }
    }
}