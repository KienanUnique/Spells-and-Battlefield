﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Systems.Scenes_Controller.Scene_Data;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;
using UnityEngine;

namespace Systems.Scenes_Controller
{
    [CreateAssetMenu(fileName = "Scenes Settings",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "Scenes Settings", order = 0)]
    public class ScenesSettings : ScriptableObject, IScenesSettings
    {
        [SerializeField] private SceneData _mainMenuScene;
        [SerializeField] private SceneData _creditsScene;
        [SerializeField] private SceneData _comicsCutsceneScene;
        [SerializeField] private List<GameLevelData> _gameLevels;
        private ReadOnlyCollection<IGameLevelData> _cachedGameLevels;

        public ReadOnlyCollection<IGameLevelData> GameLevels
        {
            get { return _cachedGameLevels ??= new List<IGameLevelData>(_gameLevels).AsReadOnly(); }
        }

        public ISceneData MainMenuScene => _mainMenuScene;
        public ISceneData CreditsScene => _creditsScene;
        public ISceneData ComicsCutsceneScene => _comicsCutsceneScene;
    }
}