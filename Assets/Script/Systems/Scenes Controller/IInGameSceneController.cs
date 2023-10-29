using Systems.Scenes_Controller.Concrete_Types;
using Systems.Scenes_Controller.Game_Level_Loot_Unlocker;

namespace Systems.Scenes_Controller
{
    public interface IInGameSceneController : IScenesController
    {
        public IGameLevelLootUnlocker CurrentGameLevelLootUnlocker { get; }
        public void LoadNextGameLevel();
        public void RestartCurrentGameLevel();
    }
}