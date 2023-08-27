using Interfaces;
using UI.Managers.In_Game;
using UI.Window.Model;

namespace UI.Menu
{
    public abstract class InGameMenuModelBase : UIWindowModelBase
    {
        protected readonly IInGameUIControllable _uiControllable;

        protected InGameMenuModelBase(IIdHolder idHolder, IInGameUIControllable uiControllable) : base(idHolder)
        {
            _uiControllable = uiControllable;
        }

        public void OnQuitMainMenuButtonPressed()
        {
            _uiControllable.RequestQuitToMainMenu();
        }

        public void OnRestartLevelMenuButtonPressed()
        {
            _uiControllable.RequestRestartLevel();
        }
    }
}