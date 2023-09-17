using System;
using System.Collections.Generic;
using Player;
using Player.Spawn;
using Player.Spell_Manager;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.In_Game.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerSpawnMarkerHolder _playerSpawnMarkerHolder;
        [SerializeField] private PlayerPrefabProvider _prefabProvider;

        public override void InstallBindings()
        {
            InstallPlayer();
        }

        private void InstallPlayer()
        {
            Container.Bind(new List<Type>
                     {
                         typeof(IPlayerInformationProvider),
                         typeof(IPlayerSpellsManagerInformation),
                         typeof(IPlayerInitializationStatus)
                     })
                     .FromComponentInNewPrefab(_prefabProvider.Prefab)
                     .AsSingle()
                     .OnInstantiated<PlayerController>(OnPlayerInstantiated)
                     .NonLazy();
        }

        private void OnPlayerInstantiated(InjectContext arg1, PlayerController playerController)
        {
            Transform playerTransform = playerController.transform;
            playerTransform.position = _playerSpawnMarkerHolder.ObjectToHold.SpawnPosition;
            playerTransform.rotation = _playerSpawnMarkerHolder.ObjectToHold.SpawnRotation;
        }
    }
}