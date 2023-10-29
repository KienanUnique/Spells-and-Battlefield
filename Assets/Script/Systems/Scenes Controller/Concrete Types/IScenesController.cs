using System;
using System.Collections.Generic;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;

namespace Systems.Scenes_Controller.Concrete_Types
{
    public interface IScenesController
    {
        public event Action LoadingNextSceneStarted;
        public IReadOnlyCollection<IGameLevelData> GameLevels { get; }
        public void LoadMainMenu();
        public void LoadGameLevel(IGameLevelData levelToLoad);
        public void LoadCredits();
    }
}