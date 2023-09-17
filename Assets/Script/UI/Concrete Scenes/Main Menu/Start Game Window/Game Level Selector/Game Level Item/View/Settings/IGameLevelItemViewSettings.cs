using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.View.Settings
{
    public interface IGameLevelItemViewSettings
    {
        public Color SelectedColor { get; }
        public Color UnselectedColor { get; }
    }
}