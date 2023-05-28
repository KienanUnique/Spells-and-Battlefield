using System.Collections.Generic;
using Enemies.Trigger;

namespace Enemies
{
    public interface IEnemyTriggersSettable
    {
        void SetExternalEnemyTargetTriggers(List<Trigger.IEnemyTargetTrigger> enemyTargetTriggers);
    }
}