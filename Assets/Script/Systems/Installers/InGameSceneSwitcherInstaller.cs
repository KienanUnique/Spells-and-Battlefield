using Systems.Scene_Switcher;
using Systems.Scene_Switcher.Concrete_Types;
using UnityEngine;
using Zenject;

namespace Systems.Installers
{
    public class InGameSceneSwitcherInstaller : MonoInstaller
    {
        [SerializeField] private ScenesSettings _scenesSettings;

        public override void InstallBindings()
        {
            var inGameSceneSwitcher = new InGameScenesSwitcher(_scenesSettings);
            Container.Bind<IInGameSceneSwitcher>().FromInstance(inGameSceneSwitcher).AsSingle();
        }
    }
}