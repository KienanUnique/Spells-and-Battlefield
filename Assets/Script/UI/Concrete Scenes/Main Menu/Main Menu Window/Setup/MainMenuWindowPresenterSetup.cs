using System.Collections.Generic;
using Systems.Scene_Switcher.Concrete_Types;
using UI.Concrete_Scenes.Main_Menu.Main_Menu_Window.Model;
using UI.Concrete_Scenes.Main_Menu.Main_Menu_Window.Presenter;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Presenter;
using UI.Window.Setup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.Main_Menu.Main_Menu_Window.Setup
{
    public class MainMenuWindowPresenterSetup : DefaultWindowPresenterSetupBase
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private StartGameWindowPresenter _startGameWindow;
        private IInitializableMainMenuWindowPresenter _presenter;
        private IScenesManager _scenesManager;

        [Inject]
        private void GetDependencies(IScenesManager scenesManager)
        {
            _scenesManager = scenesManager;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(base.ObjectsToWaitBeforeInitialization) {_startGameWindow};

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableMainMenuWindowPresenter>();
        }

        protected override void Initialize()
        {
            var model = new MainMenuWindowModel(IDHolder, Manager, _startGameWindow, _scenesManager);
            _presenter.Initialize(model, View, _startGameButton, _creditsButton, _quitButton);
        }
    }
}