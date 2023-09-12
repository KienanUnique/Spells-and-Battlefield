using System;
using Common.Abstract_Bases;
using Common.Abstract_Bases.Box_Collider_Trigger;
using Interfaces;
using ModestTree;

namespace Enemies.Spawn.Spawn_Trigger
{
    public class EnemySpawnTrigger : BoxColliderTriggerBase<IEnemyTarget>, IEnemySpawnTrigger
    {
        public event Action SpawnRequired;
        public bool IsSpawnRequired => !_requiredObjectsInside.IsEmpty();

        protected override void OnRequiredObjectEnteringDetected()
        {
            base.OnRequiredObjectEnteringDetected();
            SpawnRequired?.Invoke();
        }
    }
}