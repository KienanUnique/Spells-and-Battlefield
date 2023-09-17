using Common.Id_Holder;
using Systems.Scene_Switcher.Concrete_Types;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector;
using UI.Managers.Concrete_Types.In_Game;
using UI.Window.Model;

namespace UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Model
{
    public class StartGameWindowModel : UIWindowModelBase, IStartGameWindowModel
    {
        private readonly IGameLevelSelector _gameLevelSelector;
        private readonly IScenesManager _scenesManager;

        public StartGameWindowModel(IIdHolder idHolder, IUIWindowManager manager, IGameLevelSelector gameLevelSelector,
            IScenesManager scenesManager) : base(idHolder, manager)
        {
            _gameLevelSelector = gameLevelSelector;
            _scenesManager = scenesManager;
        }

        public override bool CanBeClosedByPlayer => true;

        public void OnBackButtonPressed()
        {
            Manager.TryCloseCurrentWindow();
        }

        public void OnLoadButtonPressed()
        {
            _scenesManager.LoadGameLevel(_gameLevelSelector.SelectedLevel);
        }
    }
}