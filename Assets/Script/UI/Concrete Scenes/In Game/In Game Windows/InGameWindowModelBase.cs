using Common.Id_Holder;
using Systems.Scene_Switcher;
using UI.Loading_Window;
using UI.Managers.Concrete_Types.In_Game;
using UI.Window.Model;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows
{
    public abstract class InGameWindowModelBase : UIWindowModelBase
    {
        protected readonly IInGameSceneSwitcher _inGameSceneSwitcher;
        private readonly ILoadingWindow _loadingWindow;

        protected InGameWindowModelBase(IIdHolder idHolder, IUIWindowManager manager,
            IInGameSceneSwitcher inGameSceneSwitcher, ILoadingWindow loadingWindow) : base(idHolder, manager)
        {
            _inGameSceneSwitcher = inGameSceneSwitcher;
            _loadingWindow = loadingWindow;
        }

        public void OnQuitMainWindowButtonPressed()
        {
            Manager.OpenWindow(_loadingWindow);
            _inGameSceneSwitcher.LoadMainMenu();
        }

        public void OnRestartLevelWindowButtonPressed()
        {
            Manager.OpenWindow(_loadingWindow);
            _inGameSceneSwitcher.RestartLevel();
        }
    }
}