using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;
using UI.Element;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector
{
    public interface IGameLevelSelector : IUIElementModel
    {
        public IGameLevelData SelectedLevel { get; }
    }
}