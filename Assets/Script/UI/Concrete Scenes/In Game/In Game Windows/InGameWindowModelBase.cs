﻿using Common.Id_Holder;
using Systems.Scenes_Controller;
using UI.Managers.Concrete_Types.In_Game;
using UI.Window.Model;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows
{
    public abstract class InGameWindowModelBase : UIWindowModelWithManagerBase
    {
        protected readonly IInGameSceneController InGameSceneController;

        protected InGameWindowModelBase(IIdHolder idHolder, IUIWindowManager manager,
            IInGameSceneController inGameSceneController) : base(idHolder, manager)
        {
            InGameSceneController = inGameSceneController;
        }

        public void OnQuitMainWindowButtonPressed()
        {
            InGameSceneController.LoadMainMenu();
        }

        public void OnRestartLevelWindowButtonPressed()
        {
            InGameSceneController.RestartCurrentGameLevel();
        }
    }
}