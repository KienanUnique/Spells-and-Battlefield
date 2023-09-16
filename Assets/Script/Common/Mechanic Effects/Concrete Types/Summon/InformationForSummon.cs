using System.Collections.Generic;
using Enemies.Trigger;
using Factions;

namespace Common.Mechanic_Effects.Concrete_Types.Summon
{
    public class InformationForSummon : IInformationForSummon
    {
        public InformationForSummon(ISummoner summoner, IFaction faction, List<IEnemyTargetTrigger> targetTriggers)
        {
            Summoner = summoner;
            Faction = faction;
            TargetTriggers = targetTriggers;
        }

        public ISummoner Summoner { get; }
        public IFaction Faction { get; }
        public List<IEnemyTargetTrigger> TargetTriggers { get; }
    }
}