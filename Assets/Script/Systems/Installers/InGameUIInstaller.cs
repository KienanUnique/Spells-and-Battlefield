using System;
using System.Collections.Generic;
using UI.Managers.In_Game;
using UI.Prefab_Provider;
using UnityEngine;
using Zenject;

namespace Systems.Installers
{
    public class InGameUIInstaller : MonoInstaller
    {
        [SerializeField] private InGameUIPrefabProvider _inGameUIPrefabProvider;

        public override void InstallBindings()
        {
            Container
                .Bind(new List<Type> {typeof(IInGameManagerUI), typeof(IInGameManagerUIInitializationStatus)})
                .FromComponentInNewPrefab(_inGameUIPrefabProvider.Prefab)
                .AsSingle()
                .NonLazy();
        }
    }
}