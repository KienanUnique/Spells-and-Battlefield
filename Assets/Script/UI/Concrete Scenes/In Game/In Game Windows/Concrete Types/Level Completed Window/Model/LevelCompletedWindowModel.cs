﻿using Common.Id_Holder;
using Systems.Scene_Switcher;
using UI.Loading_Window;
using UI.Managers.Concrete_Types.In_Game;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Level_Completed_Window.Model
{
    public class LevelCompletedWindowModel : InGameWindowModelBase, ILevelCompletedWindowModel
    {
        public LevelCompletedWindowModel(IIdHolder idHolder, IUIWindowManager manager,
            IInGameSceneManager inGameSceneManager, ILoadingWindow loadingWindow) : base(idHolder, manager,
            inGameSceneManager, loadingWindow)
        {
        }

        public override bool CanBeClosedByPlayer => false;

        public void OnLoadNextLevelButtonPressed()
        {
            InGameSceneManager.LoadNextLevel();
        }
    }
}