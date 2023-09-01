using Interfaces;
using Systems.Scene_Switcher;
using UI.Loading_Window;
using UI.Managers.Concrete_Types.In_Game;

namespace UI.In_Game_Menu.Concrete_Types.Level_Completed_Menu.Model
{
    public class LevelCompletedMenuModel : InGameMenuModelBase, ILevelCompletedMenuModel
    {
        public LevelCompletedMenuModel(IIdHolder idHolder, IUIWindowManager manager,
            IInGameSceneSwitcher inGameSceneSwitcher, ILoadingWindow loadingWindow) : base(idHolder, manager,
            inGameSceneSwitcher, loadingWindow)
        {
        }

        public override bool CanBeClosedByPlayer => false;

        public void OnLoadNextLevelButtonPressed()
        {
            _inGameSceneSwitcher.LoadNextLevel();
        }
    }
}