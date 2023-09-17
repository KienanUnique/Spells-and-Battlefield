﻿using Common.Id_Holder;
using Systems.Scene_Switcher;
using UI.Loading_Window;
using UI.Managers.Concrete_Types.In_Game;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Pause_Window.Model
{
    public class PauseWindowModel : InGameWindowModelBase, IPauseWindowModel
    {
        public PauseWindowModel(IIdHolder idHolder, IUIWindowManager manager, IInGameSceneSwitcher inGameSceneSwitcher,
            ILoadingWindow loadingWindow) : base(idHolder, manager, inGameSceneSwitcher, loadingWindow)
        {
        }

        public override bool CanBeClosedByPlayer => true;

        public void OnContinueGameButtonPressed()
        {
            Manager.TryCloseCurrentWindow();
        }
    }
}