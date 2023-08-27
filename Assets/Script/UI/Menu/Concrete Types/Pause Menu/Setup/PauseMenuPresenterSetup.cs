using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Disableable;
using UI.Menu.Concrete_Types.Pause_Menu.Model;
using UI.Menu.Concrete_Types.Pause_Menu.Presenter;
using UI.Menu.Setup;
using UnityEngine;
using UnityEngine.UI;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Menu.Concrete_Types.Pause_Menu.Setup
{
    public class PauseMenuPresenterSetup : MenuPresenterSetupBase
    {
        [SerializeField] private Button _continueGameButton;
        private IPauseMenuModel _model;
        private IInitializablePauseMenuPresenter _presenter;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            base.Prepare();
            _model = new PauseMenuModel(_idHolder, _uiControllable);
            _presenter = GetComponent<IInitializablePauseMenuPresenter>();
        }

        protected override void Initialize()
        {
            _presenter.Initialize(View, _model, new List<IDisableable>(), _restartLevelButton, _goToMainMenuButton,
                _continueGameButton);
        }
    }
}