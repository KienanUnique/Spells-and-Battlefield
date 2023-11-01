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

        public void Initialize(IGameLevelSelectorModel model)
        {
            _model = model;
            SetInitializedStatus();
            _model.SetDefaultSelection();
        }

        public IGameLevelData SelectedLevel => _model.SelectedLevel;

        public void Appear()
        {
            _model.Appear();
        }

        public void Disappear()
        {
            _model.Disappear();
        }

        protected override void SubscribeOnEvents()
        {
            foreach (IGameLevelItem levelItem in _model.LevelItems)
            {
                levelItem.Selected += OnLevelItemSelected;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (IGameLevelItem levelItem in _model.LevelItems)
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