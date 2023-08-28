using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using UI.Element.View;
using UI.Managers.In_Game;
using UI.Menu.Concrete_Types.Level_Completed_Menu.Model;
using UI.Menu.Concrete_Types.Level_Completed_Menu.Presenter;
using UI.Menu.Setup;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu.Concrete_Types.Level_Completed_Menu.Setup
{
    public class LevelCompletedMenuPresenterSetup : MenuPresenterSetupBase
    {
        [SerializeField] private Button _loadNextLevelButton;
        private ILevelCompletedMenuModel _model;
        private IInitializableLevelCompletedMenuPresenter _presenter;

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableLevelCompletedMenuPresenter>();
        }

        protected override void Initialize(IInGameUIControllable uiControllable, IUIElementView view)
        {
            _model = new LevelCompletedMenuModel(_idHolder, uiControllable);
            _presenter.Initialize(view, _model, new List<IDisableable>(), _restartLevelButton, _goToMainMenuButton,
                _loadNextLevelButton);
        }
    }
}