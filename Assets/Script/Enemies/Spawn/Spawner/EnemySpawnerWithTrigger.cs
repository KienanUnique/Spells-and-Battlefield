using System;
using System.Collections.Generic;
using Common.Abstract_Bases.Character;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Spawn;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Spawn.Factory;
using Enemies.Spawn.Spawner.Setup;
using Enemies.Trigger;

namespace Enemies.Spawn.Spawner
{
    public class EnemySpawnerWithTrigger : InitializableMonoBehaviourBase,
        IInitializableEnemySpawnerWithTrigger,
        ISpawner,
        IEnemyDeathTrigger
    {
        private IEnemyDataForSpawning _dataForSpawning;
        private IEnemyFactory _enemyFactory;
        private IPositionDataForInstantiation _positionDataForInstantiation;
        private IEnemy _spawnedEnemy;
        private List<IEnemyTargetTrigger> _targetTriggers;

        public void Initialize(IEnemyFactory enemyFactory, List<IEnemyTargetTrigger> targetTriggers,
            IPositionDataForInstantiation positionDataForInstantiation, IEnemyDataForSpawning dataForSpawning)
        {
            _enemyFactory = enemyFactory;
            _targetTriggers = targetTriggers;
            _positionDataForInstantiation = positionDataForInstantiation;
            _dataForSpawning = dataForSpawning;
            SetInitializedStatus();
        }

        public event Action SpawnedEnemyDied;

        public bool IsSpawnedEnemyDied => _spawnedEnemy is {CurrentCharacterState: CharacterState.Dead};

        public void Spawn()
        {
            if (!isActiveAndEnabled)
            {
                return;
            }

            _spawnedEnemy = _enemyFactory.Create(_dataForSpawning, _targetTriggers, _positionDataForInstantiation);
            SubscribeOnSpawnedEnemyEvents();
        }

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