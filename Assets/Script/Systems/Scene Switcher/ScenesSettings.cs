using System.Collections.Generic;
using System.Collections.ObjectModel;
using Systems.Scene_Switcher.Scene_Data;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using UnityEngine;

namespace Systems.Scene_Switcher
{
    [CreateAssetMenu(fileName = "Scenes Settings",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "Scenes Settings", order = 0)]
    public class ScenesSettings : ScriptableObject, IScenesSettings
    {
        [SerializeField] private SceneData _mainMenuScene;
        [SerializeField] private SceneData _creditsScene;
        [SerializeField] private List<GameLevelData> _gameLevels;
        private ReadOnlyCollection<IGameLevelData> _cachedGameLevels;

        public ReadOnlyCollection<IGameLevelData> GameLevels
        {
            get { return _cachedGameLevels ??= new List<IGameLevelData>(_gameLevels).AsReadOnly(); }
        }

        public ISceneData MainMenuScene => _mainMenuScene;
        public ISceneData CreditsScene => _creditsScene;
    }
}