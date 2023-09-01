using Interfaces;
using Systems.Scene_Switcher;
using UI.Loading_Window;
using UI.Managers.Concrete_Types.In_Game;

namespace UI.In_Game_Menu.Concrete_Types.Game_Over_Menu.Model
{
    public class GameOverMenuModel : InGameMenuModelBase, IGameOverMenuModel
    {
        public override bool CanBeClosedByPlayer => false;

        public GameOverMenuModel(IIdHolder idHolder, IUIWindowManager manager, IInGameSceneSwitcher inGameSceneSwitcher,
            ILoadingWindow loadingWindow) : base(idHolder, manager, inGameSceneSwitcher, loadingWindow)
        {
        }
    }
}