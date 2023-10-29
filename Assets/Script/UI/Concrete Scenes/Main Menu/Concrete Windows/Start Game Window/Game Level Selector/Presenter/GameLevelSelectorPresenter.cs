using System.Collections.Generic;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Level_Item;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Model;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Presenter
{
    public class GameLevelSelectorPresenter : InitializableMonoBehaviourBase,
        IGameLevelSelector,
        IInitializableGameLevelSelectorPresenter
    {
        private IGameLevelSelectorModel _model;
        private ICollection<IGameLevelItem> _levelItems;

        public void Initialize(IGameLevelSelectorModel model, ICollection<IGameLevelItem> levelItems)
        {
            _model = model;
            _levelItems = levelItems;
            SetInitializedStatus();
            _model.SetDefaultSelection(_levelItems);
        }

        public IGameLevelData SelectedLevel => _model.SelectedLevel;

        protected override void SubscribeOnEvents()
        {
            foreach (IGameLevelItem levelItem in _levelItems)
            {
                levelItem.Selected += OnLevelItemSelected;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (IGameLevelItem levelItem in _levelItems)
            {
                levelItem.Selected -= OnLevelItemSelected;
            }
        }

        private void OnLevelItemSelected(IGameLevelItem selectedItem)
        {
            _model.OnLevelItemSelected(selectedItem);
        }
    }
}