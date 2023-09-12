using Enemies.Setup.Settings;
using Factions;

namespace Enemies.Spawn.Data_For_Spawn
{
    public interface IEnemyDataForSpawning
    {
        public IEnemySettings Settings { get; }
        public IEnemyPrefabProvider PrefabProvider { get; }
        public IFaction Faction { get; }
    }
}