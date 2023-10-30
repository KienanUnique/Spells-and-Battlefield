using System;
using System.Collections.ObjectModel;
using Systems.Scenes_Controller.Game_Level_Loot_Unlocker;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;
using Systems.Score;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Data;
using UnityEngine;

namespace Systems.Scenes_Controller.Game_Level_Information_Storage
{
    public class GameLevelInformationStorage : IGameLevelInformationStorage
    {
        private readonly ReadOnlyCollection<IGameLevelData> _gameLevels;
        private int _currentLevelIndex;

        public GameLevelInformationStorage(ReadOnlyCollection<IGameLevelData> gameLevels)
        {
            _gameLevels = gameLevels;
        }

        public IComicsData StoredLevelComicsData => StoredLevelData.ComicsData;
        public IGameLevelStatistic StoredLevelStatistic { get; private set; }
        public IGameLevelData NextLevel => _gameLevels[_currentLevelIndex + 1];
        public IGameLevelData StoredLevelData { get; private set; }
        public IGameLevelLootUnlocker CurrentGameLevelLootUnlocker { get; private set; }
        public bool IsCurrentLevelLast => _currentLevelIndex >= _gameLevels.Count - 1;

        public void RememberLevel(IGameLevelData levelData)
        {
            StoredLevelData = levelData;
            if (StoredLevelData == null)
            {
                throw new NullReferenceException();
            }

            StoredLevelStatistic = null;
            _currentLevelIndex = _gameLevels.IndexOf(levelData);
            CurrentGameLevelLootUnlocker = new GameLevelLootUnlocker(_currentLevelIndex, _gameLevels);
        }

        public void RememberLevelStatistic(IGameLevelStatistic levelStatistic)
        {
            if (StoredLevelData == null)
            {
                throw new InvalidOperationException("Game level is not set");
            }

            if (StoredLevelStatistic == null)
            {
                throw new InvalidOperationException("Level score has already been set");
            }

            StoredLevelStatistic = levelStatistic ?? throw new NullReferenceException();
        }

        public void ForgetAllInformation()
        {
            StoredLevelStatistic = null;
            StoredLevelData = null;
            _currentLevelIndex = -1;
            CurrentGameLevelLootUnlocker = null;
        }
    }
}