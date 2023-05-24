using System.Collections.Generic;
using Enemies;
using Enemies.Factory;
using Spawn_Markers_System.Markers.Enemy_Marker;
using Zenject;

namespace Spawn_Markers_System.Spawners
{
    public class EnemySpawner : SpawnerBase<IEnemySpawnMarker>
    {
        private IEnemyFactory _enemyFactory;
        private List<IEnemyTargetTrigger> _triggerList;

        [Inject]
        private void Construct(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        protected override void Spawn()
        {
            _triggerList = new List<IEnemyTargetTrigger>();
            foreach (var marker in _markers)
            {
                _enemyFactory.Create(marker.ObjectToSpawn, _triggerList, marker.Position, marker.Rotation);
            }
        }
    }
}