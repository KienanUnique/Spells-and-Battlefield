using System.Collections.Generic;
using Enemies.Setup;
using Enemies.Setup.Settings;
using Enemies.Trigger;
using Factions;
using Interfaces;
using Pickable_Items.Data_For_Creating;

namespace Enemies
{
    public interface IEnemySetup
    {
        public IEnemy InitializedEnemy { get; }
        public void SetDataForInitialization(IEnemySettings settings, IFaction faction,
            List<IEnemyTargetTrigger> targetTriggers);
        public void SetDataForInitialization(IEnemySettings settings, IInformationForSummon informationForSummon);
    }
}