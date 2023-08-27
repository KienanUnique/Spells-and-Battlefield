using UI.Menu.Concrete_Types.Game_Over_Menu.Model;

namespace UI.Menu.Concrete_Types.Pause_Menu.Model
{
    public interface IPauseMenuModel : IInGameMenuModelBase
    {
        public void OnContinueGameButtonPressed();
    }
}