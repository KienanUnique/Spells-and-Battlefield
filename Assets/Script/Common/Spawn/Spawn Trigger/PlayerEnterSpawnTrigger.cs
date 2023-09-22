using System;
using Common.Abstract_Bases.Box_Collider_Trigger;
using Enemies;
using ModestTree;
using Player;

namespace Common.Spawn.Spawn_Trigger
{
    public class PlayerEnterSpawnTrigger : TriggerForInitializableObjectsBase<IPlayer>, ISpawnTrigger
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