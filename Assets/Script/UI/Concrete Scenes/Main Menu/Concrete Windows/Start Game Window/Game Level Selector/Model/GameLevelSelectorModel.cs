using System.Collections.Generic;
using System.Linq;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Game_Level_Item;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Model
{
    public class GameLevelSelectorModel : IGameLevelSelectorModel
    {
        private IGameLevelItem _lastSelectedLevelItem;
        public IGameLevelData SelectedLevel => _lastSelectedLevelItem.LevelData;

        public void SetDefaultSelection(ICollection<IGameLevelItem> levelItems)
        {
            levelItems.First().Select();
        }

        public void OnLevelItemSelected(IGameLevelItem selectedItem)
        {
            _lastSelectedLevelItem?.Unselect();
            _lastSelectedLevelItem = selectedItem;
        }
    }
}