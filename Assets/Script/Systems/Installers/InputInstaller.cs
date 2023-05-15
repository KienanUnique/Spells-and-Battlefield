using Player;
using Systems.Input_Manager;
using UnityEngine;
using Zenject;

namespace Systems.Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private InGameInputManager _inGameMenuInput;

        public override void InstallBindings()
        {
            InstallPlayerInput();
            InstallMenuInput();
        }

        private void InstallMenuInput()
        {
            Container
                .Bind<IMenuInput>()
                .FromInstance(_inGameMenuInput)
                .AsSingle();
        }

        private void InstallPlayerInput()
        {
            Container
                .Bind<IPlayerInput>()
                .FromInstance(_inGameMenuInput)
                .AsSingle();
        }
    }
}