﻿using Common.Id_Holder;
using Systems.Scene_Switcher.Concrete_Types;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window;
using UI.Managers.Concrete_Types.In_Game;
using UI.Window.Model;
using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.Main_Menu_Window.Model
{
    public class MainMenuWindowModel : UIWindowModelBase, IMainMenuWindowModel
    {
        private readonly IStartGameWindow _startGameWindow;
        private readonly IScenesManager _scenesManager;

        public MainMenuWindowModel(IIdHolder idHolder, IUIWindowManager manager, IStartGameWindow startGameWindow,
            IScenesManager scenesManager) : base(idHolder, manager)
        {
            _startGameWindow = startGameWindow;
            _scenesManager = scenesManager;
        }

        public override bool CanBeClosedByPlayer => false;

        public void OnStartGameButtonPressed()
        {
            Manager.OpenWindow(_startGameWindow);
        }

        public void OnCreditsButtonPressed()
        {
            _scenesManager.LoadCredits();
        }

        public void OnQuitButtonPressed()
        {
            Application.Quit();
        }
    }
}