using System;
using Common.Abstract_Bases.Box_Collider_Trigger;
using ModestTree;

namespace Enemies.Spawn.Spawn_Trigger
{
    public class EnemySpawnTrigger : TriggerForInitializableObjectsBase<IEnemyTarget>, IEnemySpawnTrigger
    {
        public event Action SpawnRequired;
        public bool IsSpawnRequired => !_requiredObjectsInside.IsEmpty();

        protected override void OnEnable()
        {
            base.OnEnable();
            RequiredObjectEnteringDetected += OnTargetEnteringDetected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            RequiredObjectEnteringDetected -= OnTargetEnteringDetected;
        }

        private void OnTargetEnteringDetected(IEnemyTarget target)
        {
            SpawnRequired?.Invoke();
        }
    }
}