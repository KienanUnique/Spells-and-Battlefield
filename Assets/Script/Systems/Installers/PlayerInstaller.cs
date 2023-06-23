using Interfaces;
using Player;
using Player.Spell_Manager;
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
                .Bind<IPlayerInformationProvider>()
                .FromInstance(_player)
                .AsSingle();

            Container
                .Bind<IPlayerSpellsManagerInformation>()
                .FromInstance(_player)
                .AsSingle();

            Container
                .Bind<IPlayerInitializationStatus>()
                .FromInstance(_player)
                .AsSingle();
        }
    }
}