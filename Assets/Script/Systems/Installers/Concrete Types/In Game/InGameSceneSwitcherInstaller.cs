using System;
using System.Collections.Generic;
using System.Linq;
using Systems.Scene_Switcher;
using Systems.Scene_Switcher.Concrete_Types;
using Systems.Scene_Switcher.Current_Game_Level_Information;
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
            InstallScenesController(inGameSceneSwitcher);

            var gameLevelLootUnlocker = new GameLevelLootUnlocker(inGameSceneSwitcher.CurrentLevel,
                inGameSceneSwitcher.GameLevels.ToArray());
            InstallGameLevelLootUnlocker(gameLevelLootUnlocker);
        }

        private void InstallGameLevelLootUnlocker(IGameLevelLootUnlocker gameLevelLootUnlocker)
        {
            Container.Bind<IGameLevelLootUnlocker>().FromInstance(gameLevelLootUnlocker).AsSingle();
        }

        private void InstallScenesController(IInGameSceneController sceneController)
        {
            Container.Bind(new List<Type> {typeof(IInGameSceneController), typeof(IScenesController)})
                     .FromInstance(sceneController)
                     .AsSingle();
        }
    }
}