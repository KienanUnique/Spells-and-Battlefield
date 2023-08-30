using System;
using System.Collections.Generic;
using Interfaces;
using Player;
using Player.Spawn;
using Player.Spell_Manager;
using UnityEngine;
using Zenject;

namespace Systems.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerSpawnMarker _playerSpawnMarker;
        [SerializeField] private Transform _playerParent;
        [SerializeField] private PlayerPrefabProvider _prefabProvider;

        public override void InstallBindings()
        {
            InstallPlayer();
        }

        private void InstallPlayer()
        {
            Container
                .Bind(new List<Type>
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
            var playerTransform = playerController.transform;
            playerTransform.SetParent(_playerParent);
            playerTransform.position = _playerSpawnMarker.SpawnPosition;
            playerTransform.rotation = _playerSpawnMarker.SpawnRotation;
        }
    }
}