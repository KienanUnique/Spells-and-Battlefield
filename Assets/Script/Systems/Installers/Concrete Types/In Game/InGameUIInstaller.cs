using System;
using System.Collections.Generic;
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
            Container.Bind(new List<Type> {typeof(IInGameManagerUI), typeof(IUIManagerInitializationStatus)})
                     .FromComponentInNewPrefab(_inGameUIPrefabProvider.Prefab)
                     .AsSingle()
                     .NonLazy();
        }
    }
}