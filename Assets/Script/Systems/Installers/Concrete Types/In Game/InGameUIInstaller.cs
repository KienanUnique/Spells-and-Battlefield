using System;
using System.Collections.Generic;
using Systems.Dialog;
using UI.Concrete_Scenes.In_Game.Prefab_Provider;
using UI.Managers.Concrete_Types.In_Game;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.In_Game
{
    public class InGameUIInstaller : MonoInstaller
    {
        [SerializeField] private InGameUIPrefabProvider _inGameUIPrefabProvider;

        public override void InstallBindings()
        {
            InstallManager();
            InstallDialogStarter();
        }

        private void InstallDialogStarter()
        {
            var dialogStarter = new DialogService();
            
            Container.Bind(new List<Type> {typeof(IDialogStarterForGameManager), typeof(IDialogService)})
                     .FromInstance(dialogStarter)
                     .AsSingle();
        }

        private void InstallManager()
        {
            Container.Bind(new List<Type> {typeof(IInGameManagerUI), typeof(IUIManagerInitializationStatus)})
                     .FromComponentInNewPrefab(_inGameUIPrefabProvider.Prefab)
                     .AsSingle()
                     .NonLazy();
        }
    }
}