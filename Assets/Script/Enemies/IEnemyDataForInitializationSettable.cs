using System.Collections.Generic;
using Common.Mechanic_Effects.Concrete_Types.Summon;
using Enemies.Setup.Settings;
using Enemies.Trigger;
using Factions;

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