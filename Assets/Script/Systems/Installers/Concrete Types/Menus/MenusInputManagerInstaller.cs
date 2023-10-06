using Systems.Input_Manager;
using Systems.Input_Manager.Concrete_Types.Menus.Prefab_Provider;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.Menus
{
    public class MenusInputManagerInstaller : MonoInstaller
    {
        [SerializeField] private MenusInputManagerPrefabProvider _menusInputManagerPrefabProvider;

        public override void InstallBindings()
        {
            Container.Bind<IInputManagerForUI>()
                     .FromComponentInNewPrefab(_menusInputManagerPrefabProvider.Prefab)
                     .AsSingle()
                     .NonLazy();
        }
    }
}