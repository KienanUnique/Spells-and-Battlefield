using Interfaces;
using UI.Managers.In_Game;

namespace UI.Menu.Concrete_Types.Level_Completed_Menu.Model
{
    public class LevelCompletedMenuModel : InGameMenuModelBase, ILevelCompletedMenuModel
    {
        public LevelCompletedMenuModel(IIdHolder idHolder, IInGameUIControllable uiControllable) : base(idHolder,
            uiControllable)
        {
        }

        public override bool CanBeClosedByPlayer => false;

        public void OnLoadNextLevelButtonPressed()
        {
            _uiControllable.RequestLoadNextLevel();
        }
    }
}