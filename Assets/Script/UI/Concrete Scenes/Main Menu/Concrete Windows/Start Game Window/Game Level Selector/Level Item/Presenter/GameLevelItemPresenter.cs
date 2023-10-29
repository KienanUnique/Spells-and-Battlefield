using System;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Level_Item.Model;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Level_Item.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Level_Item.Presenter
{
    public class GameLevelItemPresenter : InitializableMonoBehaviourBase,
        IInitializableGameLevelItem,
        IInitializableGameLevelItemPresenter
    {
        private IGameLevelItemModel _model;
        private IGameLevelItemView _view;

        public void Initialize(IGameLevelItemModel model, IGameLevelItemView view, Button selectionButton)
        {
            _model = model;
            _view = view;
            selectionButton.onClick.AddListener(model.OnClicked);
            SetInitializedStatus();
            Appear();
        }

        public event Action<IGameLevelItem> Selected;
        public IGameLevelData LevelData => _model.LevelData;

        public void Select()
        {
            _model.Select();
        }

        public void Unselect()
        {
            _model.Unselect();
        }

        public void Appear()
        {
            _view.Appear(LevelData, _model.IsSelected);
        }

        public void Disappear()
        {
            _view.Disappear();
        }

        protected override void SubscribeOnEvents()
        {
            _model.Selected += OnSelected;
            _model.Unselected += OnUnselected;
        }

        protected override void UnsubscribeFromEvents()
        {
            _model.Selected -= OnSelected;
            _model.Unselected -= OnUnselected;
        }

        private void OnSelected()
        {
            _view.Select();
            Selected?.Invoke(this);
        }

        private void OnUnselected()
        {
            _view.Unselect();
        }
    }
}