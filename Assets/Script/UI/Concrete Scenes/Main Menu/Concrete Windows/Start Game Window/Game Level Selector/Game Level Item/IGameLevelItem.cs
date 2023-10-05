using System;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using UI.Element;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Game_Level_Item
{
    public interface IGameLevelItem : IUIElement
    {
        public event Action<IGameLevelItem> Selected;
        public IGameLevelData LevelData { get; }
        public void Select();
        public void Unselect();
    }
}