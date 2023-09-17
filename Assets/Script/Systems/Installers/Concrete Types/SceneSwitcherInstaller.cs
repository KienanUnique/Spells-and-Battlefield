using Systems.Scene_Switcher;
using Systems.Scene_Switcher.Concrete_Types;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types
{
    public class SceneSwitcherInstaller : MonoInstaller
    {
        [SerializeField] protected ScenesSettings _scenesSettings;

        public override void InstallBindings()
        {
            var inGameSceneSwitcher = new ScenesManager(_scenesSettings);
            Container.Bind<IScenesManager>().FromInstance(inGameSceneSwitcher).AsSingle();
        }
    }
}