﻿using Common.Id_Holder;
using Systems.Scene_Switcher;
using UI.Loading_Window;
using UI.Managers.Concrete_Types.In_Game;

namespace UI.Concrete_Scenes.In_Game.In_Game_Windows.Concrete_Types.Game_Over_Window.Model
{
    public class GameOverWindowModel : InGameWindowModelBase, IGameOverWindowModel
    {
        public GameOverWindowModel(IIdHolder idHolder, IUIWindowManager manager, IInGameSceneSwitcher inGameSceneSwitcher,
            ILoadingWindow loadingWindow) : base(idHolder, manager, inGameSceneSwitcher, loadingWindow)
        {
        }

        public override bool CanBeClosedByPlayer => false;
    }
}