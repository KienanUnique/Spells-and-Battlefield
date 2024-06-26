﻿using Common.Id_Holder;
using Systems.Scenes_Controller.Concrete_Types;
using UI.Window.Model;

namespace UI.Concrete_Scenes.Credits.Credits_Window.Model
{
    public class CreditsWindowModel : UIWindowModelBase, ICreditsWindowModel
    {
        private readonly IScenesController _scenesController;

        public CreditsWindowModel(IIdHolder idHolder, IScenesController scenesController) : base(idHolder)
        {
            _scenesController = scenesController;
        }

        public override bool CanBeClosedByPlayer => false;

        public void OnQuitToMainMenuButtonPressed()
        {
            _scenesController.LoadMainMenu();
        }
    }
}