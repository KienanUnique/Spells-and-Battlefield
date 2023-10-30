using System;
using System.Collections.Generic;
using Systems.Scenes_Controller;
using Systems.Scenes_Controller.Concrete_Types;
using Systems.Scenes_Controller.Game_Level_Loot_Unlocker;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types
{
    public class SceneControllerInstaller : MonoInstaller
    {
        [SerializeField] private ScenesSettings _scenesSettings;

        public override void InstallBindings()
        {
            var scenesController = new ScenesController(_scenesSettings);
            InstallSceneController(scenesController);
            InstallGameLevelLootUnlocker(scenesController);
        }

        private void InstallGameLevelLootUnlocker(IInGameSceneController scenesController)
        {
            Container.Bind<IGameLevelLootUnlocker>()
                     .FromMethod(() => scenesController.CurrentGameLevelLootUnlocker)
                     .AsTransient();
        }

        private void InstallSceneController(ScenesController scenesController)
        {
            Container.Bind(new List<Type>
                     {
                         typeof(IInGameSceneController), typeof(IScenesController), typeof(IComicsToShowProvider)
                     })
                     .FromInstance(scenesController)
                     .AsSingle();
        }
    }
}