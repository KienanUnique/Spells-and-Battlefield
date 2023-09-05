using System;
using Common.Abstract_Bases;
using Interfaces;
using ModestTree;

namespace Enemies.Spawn.Spawn_Trigger
{
    public class EnemySpawnTrigger : BoxColliderTriggerBase<IEnemyTarget>, IEnemySpawnTrigger
    {
        public bool IsSpawnRequired => !_requiredObjectsInside.IsEmpty();
        public event Action SpawnRequired;

        protected override void OnRequiredObjectEnteringDetected()
        {
            base.OnRequiredObjectEnteringDetected();
            SpawnRequired?.Invoke();
        }
    }
}