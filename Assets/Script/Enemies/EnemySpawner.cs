using System.Collections.Generic;
using Common.Abstract_Bases.Spawn_Markers_System.Spawners;
using Enemies.Factory;
using Enemies.Marker;
using Enemies.Trigger;
using UnityEngine;
using Zenject;

namespace Enemies
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