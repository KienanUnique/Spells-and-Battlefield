﻿using System.Collections.Generic;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;
using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Level_Item.Factory
{
    public interface IGameLevelItemFactory
    {
        ICollection<IInitializableGameLevelItem> CreateItems(IEnumerable<IGameLevelData> levelData,
            Transform parentTransform);
    }
}