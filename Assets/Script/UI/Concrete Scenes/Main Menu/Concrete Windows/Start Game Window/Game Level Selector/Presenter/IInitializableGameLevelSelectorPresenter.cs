using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Model;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Presenter
{
    public interface IInitializableGameLevelSelectorPresenter
    {
        public void Initialize(IGameLevelSelectorModel model);
    }
}