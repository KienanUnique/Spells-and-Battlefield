using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Element.View;
using UI.Managers.In_Game;
using UI.Menu.Concrete_Types.Pause_Menu.Model;
using UI.Menu.Concrete_Types.Pause_Menu.Presenter;
using UI.Menu.Setup;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu.Concrete_Types.Pause_Menu.Setup
{
    public class PauseMenuPresenterSetup : MenuPresenterSetupBase
    {
        [SerializeField] private Button _continueGameButton;
        private IPauseMenuModel _model;
        private IInitializablePauseMenuPresenter _presenter;

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializablePauseMenuPresenter>();
        }

        protected override void Initialize(IInGameUIControllable uiControllable, IUIElementView view)
        {
            _model = new PauseMenuModel(_idHolder, uiControllable);
            _presenter.Initialize(view, _model, new List<IDisableable>(), _restartLevelButton, _goToMainMenuButton,
                _continueGameButton);
        }
    }
}