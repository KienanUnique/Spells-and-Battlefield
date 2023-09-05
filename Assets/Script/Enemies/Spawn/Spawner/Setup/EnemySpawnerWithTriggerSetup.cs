using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Spawn.Factory;
using UnityEngine;
using Zenject;
using IInitializable = Common.Abstract_Bases.Initializable_MonoBehaviour.IInitializable;

namespace Enemies.Spawn.Spawner.Setup
{
    public class EnemySpawnerWithTriggerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private EnemyDataForSpawnMarker _objectToSpawn;
        private IEnemyFactory _enemyFactory;
        private IInitializableEnemySpawnerWithTrigger _spawner;
        private IPositionDataForInstantiation _thisPositionData;

        [Inject]
        private void Construct(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            Enumerable.Empty<IInitializable>();

        protected override void Prepare()
        {
            _spawner = GetComponent<IInitializableEnemySpawnerWithTrigger>();
            _thisPositionData = new PositionDataForInstantiation(transform.position, transform.rotation);
        }

        protected override void Initialize()
        {
            _spawner.Initialize(_enemyFactory, _thisPositionData, _objectToSpawn);
        }
    }
}