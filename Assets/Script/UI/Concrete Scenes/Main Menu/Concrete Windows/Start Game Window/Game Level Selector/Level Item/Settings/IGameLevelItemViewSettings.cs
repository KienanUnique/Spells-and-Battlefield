using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Level_Item.
    Settings
{
    public interface IGameLevelItemViewSettings
    {
        public Color SelectedColor { get; }
        public Color UnselectedColor { get; }
    }
}