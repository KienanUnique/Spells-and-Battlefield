using System.Collections.Generic;
using Common.Settings.Ground_Layer_Mask;
using Enemies.Spawn.Factory;
using Enemies.Trigger;
using Factions;

namespace Interfaces
{
    public interface IInformationForSummon
    {
        public ISummoner Summoner { get; }
        public IFaction Faction { get; }
        public List<IEnemyTargetTrigger> TargetTriggers { get; }
    }

    public interface IToolsForSummon
    {
        public IEnemyFactory Factory { get; }
        public IGroundLayerMaskSetting GroundLayerMaskSetting { get; }
    }

    public class ToolsForSummon : IToolsForSummon
    {
        public ToolsForSummon(IEnemyFactory factory, IGroundLayerMaskSetting groundLayerMaskSetting)
        {
            Factory = factory;
            GroundLayerMaskSetting = groundLayerMaskSetting;
        }

        public IEnemyFactory Factory { get; }
        public IGroundLayerMaskSetting GroundLayerMaskSetting { get; }
    }
}