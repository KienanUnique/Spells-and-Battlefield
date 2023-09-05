using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Spawn.Factory;
using Enemies.Spawn.Spawner.Setup;
using Enemies.Trigger;
using Interfaces;

namespace Enemies.Spawn.Spawner
{
    public class EnemySpawnerWithTrigger : InitializableMonoBehaviourBase, IInitializableEnemySpawner,
        IInitializableEnemySpawnerWithTrigger, IEnemyDeathTrigger
    {
        private IEnemyFactory _enemyFactory;
        private IPositionDataForInstantiation _positionDataForInstantiation;
        private IEnemyDataForSpawnMarker _dataForSpawnMarker;
        private IEnemy _spawnedEnemy;

        public void Initialize(IEnemyFactory enemyFactory, IPositionDataForInstantiation positionDataForInstantiation,
            IEnemyDataForSpawnMarker dataForSpawnMarker)
        {
            _enemyFactory = enemyFactory;
            _positionDataForInstantiation = positionDataForInstantiation;
            _dataForSpawnMarker = dataForSpawnMarker;
            SetInitializedStatus();
        }

        public event Action SpawnedEnemyDied;

        public bool IsSpawnedEnemyDied => _spawnedEnemy is {CurrentCharacterState: CharacterState.Dead};

        protected override void SubscribeOnEvents()
        {
            if (_spawnedEnemy == null)
            {
                return;
            }

            SubscribeOnSpawnedEnemyEvents();
        }

        protected override void UnsubscribeFromEvents()
        {
            if (_spawnedEnemy == null)
            {
                return;
            }

            UnsubscribeFromSpawnedEnemyEvents();
        }

        public void Spawn(List<IEnemyTargetTrigger> targetTriggers)
        {
            _spawnedEnemy = _enemyFactory.Create(_dataForSpawnMarker, targetTriggers, _positionDataForInstantiation);
            SubscribeOnSpawnedEnemyEvents();
        }

        private void SubscribeOnSpawnedEnemyEvents()
        {
            _spawnedEnemy.CharacterStateChanged += OnSpawnedEnemyCharacterStateChanged;
        }

        private void UnsubscribeFromSpawnedEnemyEvents()
        {
            _spawnedEnemy.CharacterStateChanged -= OnSpawnedEnemyCharacterStateChanged;
        }

        private void OnSpawnedEnemyCharacterStateChanged(CharacterState newState)
        {
            if (newState == CharacterState.Dead)
            {
                SpawnedEnemyDied?.Invoke();
            }
        }
    }
}