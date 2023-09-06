using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Enemies.Spawn.Spawn_Trigger;
using Enemies.Spawn.Spawner;
using Enemies.Trigger;
using UnityEngine;

namespace Enemies.Spawn.Spawn_Zone_Controller.Setup
{
    public class EnemySpawnZoneControllerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private List<EnemyTargetTrigger> _triggersList;
        [SerializeField] private List<EnemySpawnTrigger> _spawnTriggers;
        private IInitializableEnemySpawnZoneController _controller;
        private List<IInitializableEnemySpawner> _spawners;

        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization =>
            new EnumerableQuery<IInitializable>(_spawners);

        protected override void Initialize()
        {
            _controller.Initialize(new List<IEnemyTargetTrigger>(_triggersList), new List<IEnemySpawner>(_spawners),
                new List<IEnemySpawnTrigger>(_spawnTriggers));
        }

        protected override void Prepare()
        {
            _controller = GetComponent<IInitializableEnemySpawnZoneController>();
            _spawners = new List<IInitializableEnemySpawner>(GetComponentsInChildren<IInitializableEnemySpawner>());
        }
    }
}