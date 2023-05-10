using Interfaces;
using Player;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerController _player;

        public override void InstallBindings()
        {
            Container
                .Bind<IPlayerInformation>()
                .FromInstance(_player)
                .AsSingle();
            
            Container
                .Bind<IEnemyTarget>()
                .FromInstance(_player)
                .AsSingle();
        }
    }
}