using System.Collections.Generic;
using Common;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using TMPro;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Model;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Presenter;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.View;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.View.Settings;
using UI.Element.Setup;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Setup
{
    public class GameLevelItemPresenterSetup : UIElementPresenterSetup, IGameLevelItemPresenterSetup
    {
        private readonly ExternalDependenciesInitializationWaiter _externalDependenciesInitializationWaiter =
            new ExternalDependenciesInitializationWaiter(true);

        [SerializeField] private Button _selectionButton;
        [SerializeField] private Transform _mainTransform;
        [SerializeField] private Image _background;
        [SerializeField] private Image _levelIcon;
        [SerializeField] private TMP_Text _levelTitle;
        private IGameLevelItemViewSettings _settings;
        private IInitializableGameLevelItemPresenter _presenter;
        private IGameLevelItemModel _model;

        [Inject]
        private void GetDependencies(IGameLevelItemViewSettings settings)
        {
            _settings = settings;
        }

        public IInitializableGameLevelItem ItemPresenter => GetComponent<IInitializableGameLevelItem>();

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new[] {_externalDependenciesInitializationWaiter};

        public void SetLevelData(IGameLevelData levelData)
        {
            _model = new GameLevelItemModel(levelData);
            _externalDependenciesInitializationWaiter.HandleExternalDependenciesInitialization();
        }

        protected override void Prepare()
        {
            _presenter = GetComponent<IInitializableGameLevelItemPresenter>();
        }

        protected override void Initialize()
        {
            var view = new GameLevelItemView(_mainTransform, DefaultUIElementViewSettings, _background, _levelIcon,
                _levelTitle, _settings);
            _presenter.Initialize(_model, view, _selectionButton);
        }
    }
}