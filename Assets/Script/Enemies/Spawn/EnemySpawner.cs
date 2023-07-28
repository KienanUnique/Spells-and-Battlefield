using System.Collections.Generic;
using Common.Abstract_Bases.Spawn_Markers_System.Spawners;
using Enemies.Spawn.Factory;
using Enemies.Spawn.Marker;
using Enemies.Trigger;
using UnityEngine;
using Zenject;

namespace Enemies.Spawn
{
    public class EnemySpawner : SpawnerBase<IEnemySpawnMarker>
    {
        [SerializeField] private List<EnemyTargetTrigger> _triggerList;
        private IEnemyFactory _enemyFactory;

        [Inject]
        private void Construct(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        protected override void Spawn()
        {
            var targetTriggers = new List<IEnemyTargetTrigger>(_triggerList);
            foreach (var marker in _markers)
            {
                _enemyFactory.Create(marker.DataForSpawn.PrefabProvider, marker.DataForSpawn.Settings, targetTriggers,
                    marker.Position, marker.Rotation);
            }
        }
    }
}