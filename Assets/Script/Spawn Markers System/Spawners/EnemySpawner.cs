using System.Collections.Generic;
using Enemies.Factory;
using Enemies.Trigger;
using Spawn_Markers_System.Markers.Enemy_Marker;
using UnityEngine;
using Zenject;

namespace Spawn_Markers_System.Spawners
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
                _enemyFactory.Create(marker.ObjectToSpawn, targetTriggers, marker.Position, marker.Rotation);
            }
        }
    }
}