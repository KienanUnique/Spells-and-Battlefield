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
        private ScenesController _scenesController;

        public override void InstallBindings()
        {
            _scenesController = new ScenesController(_scenesSettings);
            InstallSceneController();
            InstallGameLevelLootUnlocker();
        }

        private void InstallGameLevelLootUnlocker()
        {
            Container.Bind<IGameLevelLootUnlocker>()
                     .FromMethod(() => _scenesController.CurrentGameLevelLootUnlocker)
                     .AsTransient();
        }

        private void InstallSceneController()
        {
            Container.Bind(new List<Type>
                     {
                         typeof(IInGameSceneController),
                         typeof(IScenesController),
                         typeof(IComicsToShowProvider),
                         typeof(ICurrentLevelDataProvider)
                     })
                     .FromInstance(_scenesController)
                     .AsSingle()
                     .NonLazy();
        }
    }
}