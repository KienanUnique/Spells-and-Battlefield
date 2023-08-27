using Interfaces;
using UI.Managers.In_Game;

namespace UI.Menu.Concrete_Types.Pause_Menu.Model
{
    public class PauseMenuModel : InGameMenuModelBase, IPauseMenuModel
    {
        public PauseMenuModel(IIdHolder idHolder, IInGameUIControllable uiControllable) : base(idHolder, uiControllable)
        {
        }

        public override bool CanBeClosedByPlayer => true;

        public void OnContinueGameButtonPressed()
        {
            _uiControllable.TryCloseCurrentWindow();
        }
    }
}