using System.Collections.Generic;
using Systems.Scene_Switcher.Concrete_Types;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Main_Menu_Window.Model;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Main_Menu_Window.Presenter;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Presenter;
using UI.Concrete_Scenes.Main_Menu.View.Empty;
using UI.Window.Setup;
using UI.Window.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Main_Menu_Window.Setup
{
    public class MainMenuWindowPresenterSetup : WindowPresenterSetupBase
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private StartGameWindowPresenter _startGameWindow;
        private IInitializableMainMenuWindowPresenter _presenter;
        private IScenesController _scenesController;
        private IUIWindowView _view;

        [Inject]
        private void GetDependencies(IScenesController scenesController)
        {
            _scenesController = scenesController;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(base.ObjectsToWaitBeforeInitialization) {_startGameWindow};

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableMainMenuWindowPresenter>();
            _view = new EmptyUIWindowView();
        }

        protected override void Initialize()
        {
            var model = new MainMenuWindowModel(IDHolder, Manager, _startGameWindow, _scenesController);
            _presenter.Initialize(model, _view, _startGameButton, _creditsButton, _quitButton);
        }
    }
}