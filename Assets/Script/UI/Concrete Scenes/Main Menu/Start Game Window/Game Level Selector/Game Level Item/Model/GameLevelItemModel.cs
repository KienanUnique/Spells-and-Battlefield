using System;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;

namespace UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Model
{
    public class GameLevelItemModel : IGameLevelItemModel
    {
        public GameLevelItemModel(IGameLevelData levelData)
        {
            LevelData = levelData;
        }

        public event Action Selected;
        public event Action Unselected;
        public IGameLevelData LevelData { get; }
        public bool IsSelected { get; private set; }

        public void OnClicked()
        {
            Select();
        }

        public void Select()
        {
            if (IsSelected)
            {
                return;
            }

            IsSelected = true;
            Selected?.Invoke();
        }

        public void Unselect()
        {
            if (!IsSelected)
            {
                return;
            }

            IsSelected = false;
            Unselected?.Invoke();
        }
    }
}