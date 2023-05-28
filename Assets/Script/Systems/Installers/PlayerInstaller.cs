using Interfaces;
using Player;
using UnityEngine;
using Zenject;

namespace Systems.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _player;

        public override void InstallBindings()
        {
            InstallPlayerInformation();
        }

        private void InstallPlayerInformation()
        {
            Container
                .Bind<IPlayerInformation>()
                .FromInstance(_player)
                .AsSingle();
        }
    }
}