using System;
using System.Collections.Generic;
using System.Linq;
using Enemies.Spawn.Spawner;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spawned_Enemy_Death
{
    public class SpawnedEnemiesDeathMechanismTrigger : MechanismsTriggerBase,
        IInitializableSpawnedEnemiesDeathMechanismTrigger
    {
        private List<IEnemyDeathTrigger> _triggers;
        private bool _wasTriggered;

        public void Initialize(List<IEnemyDeathTrigger> triggers)
        {
            _triggers = triggers;
            SetInitializedStatus();
            if (IsAllEnemiesDead && !_wasTriggered)
            {
                Trigger();
            }
        }

        public override event Action Triggered;

        private bool IsAllEnemiesDead => _triggers.All(trigger => trigger.IsSpawnedEnemyDied);

        protected override void SubscribeOnEvents()
        {
            if (_wasTriggered)
            {
                return;
            }

            foreach (IEnemyDeathTrigger enemyDeathTrigger in _triggers)
            {
                enemyDeathTrigger.SpawnedEnemyDied += OnSpawnedEnemyDied;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            foreach (IEnemyDeathTrigger enemyDeathTrigger in _triggers)
            {
                enemyDeathTrigger.SpawnedEnemyDied -= OnSpawnedEnemyDied;
            }
        }

        private void OnSpawnedEnemyDied()
        {
            if (IsAllEnemiesDead && !_wasTriggered)
            {
                Trigger();
            }
        }

        private void Trigger()
        {
            if (_wasTriggered)
            {
                return;
            }

            _wasTriggered = true;
            Triggered?.Invoke();
        }
    }
}