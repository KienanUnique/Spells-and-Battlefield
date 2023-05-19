using System.Collections.Generic;

namespace Enemies
{
    public interface IEnemyTriggersSettable
    {
        void SetExternalEnemyTargetTriggers(List<IEnemyTargetTrigger> enemyTargetTriggers);
    }
}