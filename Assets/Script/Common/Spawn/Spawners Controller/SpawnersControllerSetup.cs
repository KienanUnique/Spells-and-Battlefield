using System.Collections.Generic;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Spawn.Spawn_Trigger;
using UnityEngine;

namespace Common.Spawn.Spawners_Controller
{
    public class SpawnersControllerSetup : SetupMonoBehaviourBase
    {
        [SerializeField] private List<PlayerEnterSpawnTrigger> _spawnTriggers;
        private List<ISpawner> _spawners;
        private IInitializableSpawnersController _controller;
        protected override IEnumerable<IInitializable> ObjectsToWaitBeforeInitialization => _spawners;

        protected override void Prepare()
        {
            _spawners = new List<ISpawner>(GetComponentsInChildren<ISpawner>());
            _controller = GetComponent<IInitializableSpawnersController>();
        }

        protected override void Initialize()
        {
            _controller.Initialize(new List<ISpawnTrigger>(_spawnTriggers), _spawners);
        }
    }
}