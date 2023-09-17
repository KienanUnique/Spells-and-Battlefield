using Systems.Scene_Switcher.Concrete_Types;

namespace Systems.Scene_Switcher
{
    public interface IInGameSceneSwitcher : IScenesSwitcher
    {
        public void LoadNextLevel();
        public void RestartLevel();
    }
}