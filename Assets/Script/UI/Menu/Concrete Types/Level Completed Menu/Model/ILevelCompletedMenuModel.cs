using UI.Menu.Concrete_Types.Game_Over_Menu.Model;

namespace UI.Menu.Concrete_Types.Level_Completed_Menu.Model
{
    public interface ILevelCompletedMenuModel : IInGameMenuModelBase
    {
        public void OnLoadNextLevelButtonPressed();
    }
}