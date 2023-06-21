using UnityEngine;

namespace Settings.Enemy
{
    [CreateAssetMenu(fileName = "Enemy Settings",
        menuName = ScriptableObjectsMenuDirectories.GeneralSettingsDirectory + "Enemy Settings", order = 0)]
    public class GeneralEnemySettings : ScriptableObject
    {
        [Min(1f)] [SerializeField] private float _delayInSecondsBeforeDestroy = 1f;
        [SerializeField] private Vector3 _spawnSpellOffset = new Vector3(0, 3f, 0);

        public Vector3 SpawnSpellOffset => _spawnSpellOffset;
        public float DelayInSecondsBeforeDestroy => _delayInSecondsBeforeDestroy;
    }
}