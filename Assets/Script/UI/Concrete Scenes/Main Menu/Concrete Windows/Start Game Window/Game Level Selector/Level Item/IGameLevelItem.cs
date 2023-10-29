using System;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;
using UI.Element;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Level_Item
{
    public interface IGameLevelItem : IUIElementModel
    {
        public event Action<IGameLevelItem> Selected;
        public IGameLevelData LevelData { get; }
        public void Select();
        public void Unselect();
    }
}