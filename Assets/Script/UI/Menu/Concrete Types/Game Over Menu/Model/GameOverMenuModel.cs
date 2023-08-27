using Interfaces;
using UI.Managers.In_Game;

namespace UI.Menu.Concrete_Types.Game_Over_Menu.Model
{
    public class GameOverMenuModel : InGameMenuModelBase, IGameOverMenuModel
    {
        public GameOverMenuModel(IIdHolder idHolder, IInGameUIControllable uiControllable) : base(idHolder,
            uiControllable)
        {
        }

        public override bool CanBeClosedByPlayer => false;
    }
}