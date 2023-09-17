﻿using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;

namespace UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.View
{
    public interface IGameLevelItemView
    {
        public void Appear(IGameLevelData levelData, bool isSelected);
        public void Disappear();
        public void Select();
        public void Unselect();
    }
}