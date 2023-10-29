using Common.Id_Holder;
using Systems.Scenes_Controller.Concrete_Types;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window;
using UI.Managers.Concrete_Types.In_Game;
using UI.Window.Model;
using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Main_Menu_Window.Model
{
    public class MainMenuWindowModel : UIWindowModelWithManagerBase, IMainMenuWindowModel
    {
        private readonly IStartGameWindow _startGameWindow;
        private readonly IScenesController _scenesController;

        public MainMenuWindowModel(IIdHolder idHolder, IUIWindowManager manager, IStartGameWindow startGameWindow,
            IScenesController scenesController) : base(idHolder, manager)
        {
            _startGameWindow = startGameWindow;
            _scenesController = scenesController;
        }

        public override bool CanBeClosedByPlayer => false;

        public void OnStartGameButtonPressed()
        {
            Manager.OpenWindow(_startGameWindow);
        }

        public void OnCreditsButtonPressed()
        {
            _scenesController.LoadCredits();
        }

        public void OnQuitButtonPressed()
        {
            Application.Quit();
        }
    }
}