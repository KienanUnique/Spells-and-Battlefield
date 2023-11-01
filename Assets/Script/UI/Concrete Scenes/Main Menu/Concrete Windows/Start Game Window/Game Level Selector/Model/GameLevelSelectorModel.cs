using System.Collections.Generic;
using System.Linq;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Level_Item;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Model
{
    public class GameLevelSelectorModel : IGameLevelSelectorModel
    {
        private IGameLevelItem _lastSelectedLevelItem;

        public GameLevelSelectorModel(ICollection<IGameLevelItem> levelItems)
        {
            LevelItems = levelItems;
        }

        public IGameLevelData SelectedLevel => _lastSelectedLevelItem.LevelData;

        public ICollection<IGameLevelItem> LevelItems { get; }

        public void SetDefaultSelection()
        {
            IGameLevelItem selectedItem = LevelItems.First();
            _lastSelectedLevelItem = selectedItem;
            selectedItem.Select();
        }

        public void OnLevelItemSelected(IGameLevelItem selectedItem)
        {
            if (selectedItem == _lastSelectedLevelItem)
            {
                return;
            }

            _lastSelectedLevelItem?.Unselect();
            _lastSelectedLevelItem = selectedItem;
        }

        public void Appear()
        {
            foreach (IGameLevelItem levelItem in LevelItems)
            {
                levelItem.Appear();
            }
        }

        public void Disappear()
        {
            foreach (IGameLevelItem levelItem in LevelItems)
            {
                levelItem.Disappear();
            }
        }
    }
}