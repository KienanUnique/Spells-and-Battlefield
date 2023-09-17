using System.Collections.Generic;
using Systems.Scene_Switcher.Concrete_Types;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Presenter;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Model;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Presenter;
using UI.Window.Setup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Setup
{
    public class StartGameWindowPresenterSetup : DefaultWindowPresenterSetupBase
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private GameLevelSelectorPresenter _gameLevelSelector;
        private IScenesManager _scenesManager;
        private IInitializableStartGameWindowPresenter _presenter;

        [Inject]
        private void GetDependencies(IScenesManager scenesManager)
        {
            _scenesManager = scenesManager;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new List<IInitializable>(base.ObjectsToWaitBeforeInitialization) {_gameLevelSelector};

        protected override void Prepare()
        {
            base.Prepare();
            _presenter = GetComponent<IInitializableStartGameWindowPresenter>();
        }

        protected override void Initialize()
        {
            var model = new StartGameWindowModel(IDHolder, Manager, _gameLevelSelector, _scenesManager);
            _presenter.Initialize(model, View, _backButton, _loadButton);
        }
    }
}