using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Level_Item.Setup
{
    public interface IGameLevelItemPresenterSetup
    {
        public IInitializableGameLevelItem ItemPresenter { get; }
        public void SetLevelData(IGameLevelData levelData);
    }
}