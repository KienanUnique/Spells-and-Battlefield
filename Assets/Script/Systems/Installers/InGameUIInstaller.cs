using UI.Managers.In_Game;
using UnityEngine;
using Zenject;

namespace Systems.Installers
{
    public class InGameUIInstaller : MonoInstaller
    {
        [SerializeField] private InGameManagerUI _inGameManagerUI;

        public override void InstallBindings()
        {
            InstallInGameManager();
        }

        private void InstallInGameManager()
        {
            Container
                .Bind<IInGameUIControllable>()
                .FromInstance(_inGameManagerUI)
                .AsSingle();
        }
    }
}