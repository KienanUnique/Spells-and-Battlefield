using System.Collections.Generic;
using Enemies.Setup;
using Enemies.Trigger;
using Interfaces;
using Pickable_Items.Data_For_Creating;

namespace Enemies
{
    public interface IEnemySetup
    {
        public IEnemy InitializedEnemy { get; }
        public void SetDataForInitialization(IEnemySettings settings, IPickableItemDataForCreating itemToDrop, List<IEnemyTargetTrigger> targetTriggers);
    }
}