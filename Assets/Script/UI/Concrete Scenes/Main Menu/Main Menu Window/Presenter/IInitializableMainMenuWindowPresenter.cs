﻿using UI.Concrete_Scenes.Main_Menu.Main_Menu_Window.Model;
using UI.Element.View;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Main_Menu.Main_Menu_Window.Presenter
{
    public interface IInitializableMainMenuWindowPresenter
    {
        public void Initialize(IMainMenuWindowModel model, IUIElementView view, Button startGameButton,
            Button creditsButton, Button quitButton);
    }
}