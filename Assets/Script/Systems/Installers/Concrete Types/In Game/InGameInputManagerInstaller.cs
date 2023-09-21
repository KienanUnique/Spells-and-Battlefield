using System;
using System.Collections.Generic;
using Systems.Input_Manager;
using Systems.Input_Manager.Concrete_Types.In_Game;
using Systems.Input_Manager.Concrete_Types.In_Game.Prefab_Provider;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.In_Game
{
    public class InGameInputManagerInstaller : MonoInstaller
    {
        [SerializeField] private InGameInputManagerPrefabProvider _inGameMenuInputProvider;

        public override void InstallBindings()
        {
            Container.Bind(new List<Type>
                     {
                         typeof(IPlayerInput), typeof(IInGameSystemInputManager), typeof(IInputManagerForUI)
                     })
                     .FromComponentInNewPrefab(_inGameMenuInputProvider.Prefab)
                     .AsSingle()
                     .NonLazy();
        }
    }
}