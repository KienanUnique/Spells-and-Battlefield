using System.Collections.Generic;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Level_Item;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Model
{
    public interface IGameLevelSelectorModel : IGameLevelSelector
    {
        public ICollection<IGameLevelItem> LevelItems { get; }
        public void SetDefaultSelection();
        public void OnLevelItemSelected(IGameLevelItem selectedItem);
    }
}