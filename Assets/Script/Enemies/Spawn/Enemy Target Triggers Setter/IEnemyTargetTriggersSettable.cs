using System.Collections.Generic;
using Enemies.Trigger;

namespace Enemies.Spawn.Enemy_Target_Triggers_Setter
{
    public interface IEnemyTargetTriggersSettable
    {
        public void SetTargetTriggers(List<IEnemyTargetTrigger> targetTriggers);
    }
}