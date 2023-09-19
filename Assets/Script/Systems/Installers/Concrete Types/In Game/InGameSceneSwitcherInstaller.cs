using System;
using System.Collections.Generic;
using Systems.Scene_Switcher;
using Systems.Scene_Switcher.Concrete_Types;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.In_Game
{
    public class InGameSceneSwitcherInstaller : MonoInstaller
    {
        [SerializeField] private ScenesSettings _scenesSettings;

        public override void InstallBindings()
        {
            var inGameSceneSwitcher = new InGameScenesController(_scenesSettings);

            Container.Bind(new List<Type> {typeof(IInGameSceneController), typeof(IScenesController)})
                     .FromInstance(inGameSceneSwitcher)
                     .AsSingle();
        }
    }
}