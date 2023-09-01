using Interfaces;
using Systems.Scene_Switcher;
using UI.Loading_Window;
using UI.Managers.Concrete_Types.In_Game;
using UI.Window.Model;

namespace UI.In_Game_Menu
{
    public abstract class InGameMenuModelBase : UIWindowModelBase
    {
        protected readonly IInGameSceneSwitcher _inGameSceneSwitcher;
        private readonly ILoadingWindow _loadingWindow;

        protected InGameMenuModelBase(IIdHolder idHolder, IUIWindowManager manager,
            IInGameSceneSwitcher inGameSceneSwitcher, ILoadingWindow loadingWindow) : base(idHolder, manager)
        {
            _inGameSceneSwitcher = inGameSceneSwitcher;
            _loadingWindow = loadingWindow;
        }

        public void OnQuitMainMenuButtonPressed()
        {
            Manager.OpenWindow(_loadingWindow);
            _inGameSceneSwitcher.LoadMainMenu();
        }

        public void OnRestartLevelMenuButtonPressed()
        {
            Manager.OpenWindow(_loadingWindow);
            _inGameSceneSwitcher.RestartLevel();
        }
    }
}