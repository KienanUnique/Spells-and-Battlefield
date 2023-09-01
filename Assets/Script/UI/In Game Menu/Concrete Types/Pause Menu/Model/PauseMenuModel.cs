using Interfaces;
using Systems.Scene_Switcher;
using UI.Loading_Window;
using UI.Managers.Concrete_Types.In_Game;

namespace UI.In_Game_Menu.Concrete_Types.Pause_Menu.Model
{
    public class PauseMenuModel : InGameMenuModelBase, IPauseMenuModel
    {
        public PauseMenuModel(IIdHolder idHolder, IUIWindowManager manager, IInGameSceneSwitcher inGameSceneSwitcher,
            ILoadingWindow loadingWindow) : base(idHolder, manager,
            inGameSceneSwitcher, loadingWindow)
        {
        }

        public override bool CanBeClosedByPlayer => true;

        public void OnContinueGameButtonPressed()
        {
            Manager.TryCloseCurrentWindow();
        }
    }
}