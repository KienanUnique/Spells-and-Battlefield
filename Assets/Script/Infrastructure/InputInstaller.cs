using Player;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private InGameInputManager _inGameMenuInput;

        public override void InstallBindings()
        {
            Container
                .Bind<IPlayerInput>()
                .FromInstance(_inGameMenuInput)
                .AsSingle();

            Container
                .Bind<IMenuInput>()
                .FromInstance(_inGameMenuInput)
                .AsSingle();
        }
    }
}