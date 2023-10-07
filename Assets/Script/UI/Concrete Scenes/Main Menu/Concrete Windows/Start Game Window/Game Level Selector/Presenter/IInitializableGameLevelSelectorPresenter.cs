﻿using System.Collections.Generic;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Level_Item;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Model;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Presenter
{
    public interface IInitializableGameLevelSelectorPresenter
    {
        void Initialize(IGameLevelSelectorModel model, ICollection<IGameLevelItem> levelItems);
    }
}