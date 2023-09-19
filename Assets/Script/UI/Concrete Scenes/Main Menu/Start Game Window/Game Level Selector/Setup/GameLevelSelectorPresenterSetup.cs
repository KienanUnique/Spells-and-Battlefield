using System.Collections.Generic;
using Common.Abstract_Bases;
using Systems.Scene_Switcher.Concrete_Types;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Factory;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Model;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Presenter;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Setup
{
    public class GameLevelSelectorPresenterSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private Transform _scrollView;
        private IGameLevelSelectorModel _model;
        private IInitializableGameLevelSelectorPresenter _presenter;
        private IScenesController _scenesController;
        private IGameLevelItemFactory _gameLevelItemFactory;
        private IReadOnlyCollection<IGameLevelData> _gameLevels;
        private ICollection<IInitializableGameLevelItem> _levelItems;

        [Inject]
        private void GetDependencies(IScenesController scenesController, IGameLevelItemFactory gameLevelItemFactory)
        {
            _gameLevels = scenesController.GameLevels;
            _gameLevelItemFactory = gameLevelItemFactory;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization => _levelItems;

        protected override void Prepare()
        {
            _model = new GameLevelSelectorModel();
            _presenter = GetComponent<IInitializableGameLevelSelectorPresenter>();
            _levelItems = _gameLevelItemFactory.CreateItems(_gameLevels, _scrollView);
        }

        protected override void Initialize()
        {
            _presenter.Initialize(_model, new List<IGameLevelItem>(_levelItems));
        }
    }
}