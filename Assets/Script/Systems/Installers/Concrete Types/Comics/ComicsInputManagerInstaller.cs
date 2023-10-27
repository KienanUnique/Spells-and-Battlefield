using System;
using System.Collections.Generic;
using Systems.Input_Manager;
using Systems.Input_Manager.Concrete_Types.Comics_Cutscene;
using Systems.Input_Manager.Concrete_Types.Comics_Cutscene.Prefab_Provider;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.Comics
{
    public class ComicsInputManagerInstaller : MonoInstaller
    {
        [SerializeField] private ComicsCutsceneInputManagerPrefabProvider _cutsceneInputManager;

        public override void InstallBindings()
        {
            Container.Bind(new List<Type> {typeof(IComicsCutsceneInputManager), typeof(IInputManagerForUI)})
                     .FromComponentInNewPrefab(_cutsceneInputManager.Prefab)
                     .AsSingle()
                     .NonLazy();
        }
    }
}