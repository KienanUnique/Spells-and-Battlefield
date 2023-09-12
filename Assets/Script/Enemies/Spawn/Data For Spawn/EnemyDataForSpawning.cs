using Enemies.Prefab_Provider;
using Enemies.Setup.Settings;
using Factions;
using UnityEngine;

namespace Enemies.Spawn.Data_For_Spawn
{
    [CreateAssetMenu(fileName = "Enemy Data For Spawning",
        menuName = ScriptableObjectsMenuDirectories.EnemiesDirectory + "Enemy Data For Spawning", order = 0)]
    public class EnemyDataForSpawning : ScriptableObject, IEnemyDataForSpawning
    {
        [SerializeField] private EnemyPrefabProvider _prefabProvider;
        [SerializeField] private EnemySettings _settings;
        [SerializeField] private FactionScriptableObject _faction;

        public IEnemySettings Settings => _settings;
        public IEnemyPrefabProvider PrefabProvider => _prefabProvider;
        public IFaction Faction => _faction;
    }
}