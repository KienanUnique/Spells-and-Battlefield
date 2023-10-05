using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Model;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Game_Level_Item.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Presenter
{
    public interface IInitializableGameLevelItemPresenter
    {
        public void Initialize(IGameLevelItemModel model, IGameLevelItemView view, Button selectionButton);
    }
}