using System.Collections.Generic;
using Enemies.Setup;
using Enemies.Trigger;

namespace Enemies
{
    public interface IEnemyDataForInitializationSettable
    {
        public void SetDataForInitialization(IEnemySettings settings, List<IEnemyTargetTrigger> targetTriggers);
    }
}