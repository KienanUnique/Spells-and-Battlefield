using Pickable_Items;
using UnityEngine;

namespace General_Settings_in_Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Enemy Settings",
        menuName = "Spells and Battlefield/Settings/General Settings/Enemy Settings", order = 0)]
    public class GeneralEnemySettings : ScriptableObject
    {
        [Min(1f)] [SerializeField] private float _delayInSecondsBeforeDestroy = 1f;
        [SerializeField] private Vector3 _spawnSpellOffset = new Vector3(0, 3f, 0);

        public Vector3 SpawnSpellOffset => _spawnSpellOffset;
        public float DelayInSecondsBeforeDestroy => _delayInSecondsBeforeDestroy;
    }
}