using System;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;

namespace UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Model
{
    public interface IGameLevelItemModel
    {
        public event Action Selected;
        public event Action Unselected;
        public IGameLevelData LevelData { get; }
        public bool IsSelected { get; }
        public void OnClicked();
        public void Select();
        public void Unselect();
    }
}