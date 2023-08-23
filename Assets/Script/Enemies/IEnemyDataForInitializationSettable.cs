using System.Collections.Generic;
using Enemies.Setup;
using Enemies.Trigger;
using Pickable_Items.Data_For_Creating;

namespace Enemies
{
    public interface IEnemyDataForInitializationSettable
    {
        public void SetDataForInitialization(IEnemySettings settings, IPickableItemDataForCreating itemToDrop, List<IEnemyTargetTrigger> targetTriggers);
    }
}