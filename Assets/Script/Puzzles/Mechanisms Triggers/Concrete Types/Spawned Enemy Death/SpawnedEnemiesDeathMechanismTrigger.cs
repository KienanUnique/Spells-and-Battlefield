using System.Collections.Generic;
using System.Linq;
using Enemies.Spawn.Spawner;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spawned_Enemy_Death
{
    public class SpawnedEnemiesDeathMechanismTrigger : MechanismsTriggerBase,
        IInitializableSpawnedEnemiesDeathMechanismTrigger
    {
        private List<IEnemyDeathTrigger> _triggers;

        public void Initialize(List<IEnemyDeathTrigger> triggers, MechanismsTriggerBaseSetupData baseSetupData)
        {
            _triggers = triggers;
            InitializeBase(baseSetupData);
            if (IsAllEnemiesDead && !WasTriggered)
            {
                Trigger();
            }
        }
        
        private bool IsAllEnemiesDead => _triggers.All(trigger => trigger.IsSpawnedEnemyDied);

        protected override void SubscribeOnEvents()
        {
            if (WasTriggered)
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
            if (IsAllEnemiesDead && !WasTriggered)
            {
                Trigger();
            }
        }

        private void Trigger()
        {
            if (WasTriggered)
            {
                return;
            }

            TryInvokeTriggerEvent();
        }
    }
}